using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using static Game.Assets.Scripts.Game.Logic.Presenters.Ui.ScreenManagerPresenter;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class GhostPresenter : BasePresenter<IGhostView>
    {
        public Action OnGhostPostionChanged = delegate { };
        public ConstructionDefinition Definition { get; private set; }
        public FieldRotation Rotation { get; private set; }
        
        private readonly IGhostView _view;
        private readonly IGameTime _time;
        private readonly ConstructionsSettingsDefinition _constructionsSettings;
        private readonly IControls _controls;
        private readonly IGameKeysManager _gameKeysManager;
        private readonly BuildScreenPresenter _buildScreen;
        private readonly PlacementField _constructionsManager;
        private readonly ScreenManagerPresenter _screenManager;
        private readonly IAssets _assets;
        private FloatPoint _pointerPosition;
        private KeyCommand _rotateLeft;
        private KeyCommand _rotateRight;
        private IConstructionModelView _modelView;

        public GhostPresenter(ConstructionsSettingsDefinition constructionsSettings, 
            ScreenManagerPresenter screenManager,
            PlacementField constructionsManager,
            BuildScreenPresenter buildScreen, IControls controls, IGameKeysManager gameKeysManager, IAssets assets, IGhostView view, IGameTime time) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _time = time;
            _constructionsSettings = constructionsSettings ?? throw new ArgumentNullException(nameof(constructionsSettings));
            _controls = controls ?? throw new ArgumentNullException(nameof(controls));
            _gameKeysManager = gameKeysManager ?? throw new ArgumentNullException(nameof(gameKeysManager));
            _buildScreen = buildScreen ?? throw new ArgumentNullException(nameof(buildScreen));
            _constructionsManager = constructionsManager ?? throw new ArgumentNullException(nameof(constructionsManager));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            Definition = _buildScreen.CurrentCard.Definition ?? throw new ArgumentNullException(nameof(_buildScreen.CurrentCard.Definition));

            _rotateLeft = _gameKeysManager.GetKey(GameKeys.RotateLeft);
            _rotateRight = _gameKeysManager.GetKey(GameKeys.RotateRight);

            _view.Container.Clear();
            _modelView = _view.Container.Spawn<IConstructionModelView>(_assets.GetPrefab(_buildScreen.CurrentCard.Definition.LevelViewPath));
            _modelView.Animator.Play(IConstructionModelView.Animations.Dragging.ToString());

            _rotateLeft.OnTap += HandleRotateLeftTap;
            _rotateRight.OnTap += HandleRotateRightTap;

            _controls.OnLevelClick += HandleOnLevelClick;
            _controls.OnLevelPointerMoved += HandleOnPointerMoved;

            HandleOnPointerMoved(_controls.PointerLevelPosition);
        }

        protected override void DisposeInner()
        {
            _controls.OnLevelClick -= HandleOnLevelClick;
            _controls.OnLevelPointerMoved -= HandleOnPointerMoved;

            _rotateLeft.OnTap -= HandleRotateLeftTap;
            _rotateRight.OnTap -= HandleRotateRightTap;
        }

        public IntPoint GetLocalPosition(PlacementField placementField)
        {
            return GetWorldCellPosition() + placementField.Offset;
        }

        public IntPoint GetWorldCellPosition()
        {
            var position = new FieldPositionsCalculator(_constructionsSettings.CellSize, 
                _buildScreen.CurrentCard.Definition.GetRect(Rotation));

            return position.GetWorldCellPosition(_pointerPosition);
        }

        public FloatPoint GetWorldPosition()
        {
            var position = new FieldPositionsCalculator(_constructionsSettings.CellSize,
                _buildScreen.CurrentCard.Definition.GetRect(Rotation));

            return position.GetPositionByWorldPosition(position.GetWorldCellPosition(_pointerPosition));
        }

        public FloatPoint GetTargetPosition()
        {
            return _pointerPosition;
        }

        private void HandleOnLevelClick()
        {
            var field = _constructionsManager;
            if (field.CanPlace(_buildScreen.CurrentCard, GetLocalPosition(field), Rotation))
            {
                var points =_constructionsManager.GetPoints(_buildScreen.CurrentCard.Definition,
                    GetLocalPosition(_constructionsManager), Rotation);

                var construction = field.Build(_buildScreen.CurrentCard, GetLocalPosition(field), Rotation);
                var curve = new BezierCurve(construction.GetViewPosition(),
                    IPointAttractionPositionView.Default.PointsAttractionPoint.Value,
                    construction.GetViewPosition() + new FloatPoint3D(0, 4, 0),
                    IPointAttractionPositionView.Default.PointsAttractionControlPoint.Value);

                new BuildingPointsAnimation(curve, points,
                    IPointPieceSpawnerPresenter.Default,
                    IDefinitions.Default.Get<ConstructionsSettingsDefinition>(), _time).Play();
            }
            _screenManager.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

        private void HandleOnPointerMoved(FloatPoint worldPosition)
        {
            _pointerPosition = worldPosition;
            UpdatePosition();
        }

        private bool CanPlace()
    {
            if (_constructionsManager.CanPlace(_buildScreen.CurrentCard, GetLocalPosition(_constructionsManager), Rotation))
            {
                return true;
            }
            return false;
        }

        private void UpdatePoints()
        {
            var points = 0;
            points += _constructionsManager.GetPoints(_buildScreen.CurrentCard.Definition, 
                GetLocalPosition(_constructionsManager), Rotation);

            _buildScreen.UpdatePoints(GetViewPosition(), points, 
                _constructionsManager
                .GetAdjacencyPoints(_buildScreen.CurrentCard.Definition, GetLocalPosition(_constructionsManager), Rotation));
        }

        private void HandleRotateLeftTap()
        {
            Rotation = ConstructionRotation.RotateLeft(Rotation);
            UpdatePosition();
        }

        private void HandleRotateRightTap()
        {
            Rotation = ConstructionRotation.RotateRight(Rotation);
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            _modelView.BorderAnimator.Play(CanPlace() ? IConstructionModelView.BorderAnimations.Idle.ToString() : IConstructionModelView.BorderAnimations.Disallowed.ToString());
            _view.Rotator.Look(ConstructionRotation.ToDirection(Rotation));
            _view.LocalPosition.Value = GetViewPosition();
            UpdatePoints();
            OnGhostPostionChanged();
        }

        private FloatPoint GetViewPosition()
        {
            var position = new FieldPositionsCalculator(_constructionsSettings.CellSize,
                _buildScreen.CurrentCard.Definition.GetRect(Rotation));

            return position.GetWorldPositionInGrid(_pointerPosition);
        }
    }
}
