using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Level;
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
        private readonly ConstructionsSettingsDefinition _constructionsSettings;
        private readonly IControls _controls;
        private readonly IGameKeysManager _gameKeysManager;
        private readonly BuildScreenPresenter _buildScreen;
        private readonly ConstructionsManager _constructionsManager;
        private readonly ScreenManagerPresenter _screenManager;
        private readonly IAssets _assets;
        private FloatPoint _pointerPosition;
        private KeyCommand _rotateLeft;
        private KeyCommand _rotateRight;
        private IConstructionModelView _modelView;

        public GhostPresenter(ConstructionsSettingsDefinition constructionsSettings, 
            ScreenManagerPresenter screenManager,
            ConstructionsManager constructionsManager,
            BuildScreenPresenter buildScreen, IControls controls, IGameKeysManager gameKeysManager, IAssets assets, IGhostView view) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
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

            UpdatePoints();
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

        private void HandleOnLevelClick()
        {
            foreach (var field in _constructionsManager.Placements)
            {
                if (field.CanPlace(_buildScreen.CurrentCard, GetLocalPosition(field), Rotation))
                {
                    field.Build(_buildScreen.CurrentCard, GetLocalPosition(field), Rotation);
                    break;
                }
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
            foreach (var field in _constructionsManager.Placements)
            {
                if (field.CanPlace(_buildScreen.CurrentCard, GetLocalPosition(field), Rotation))
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdatePoints()
        {
            var points = 0;
            foreach (var field in _constructionsManager.Placements)
            {
                points += field.GetPoints(_buildScreen.CurrentCard.Definition, GetLocalPosition(field), Rotation);
            }

            _buildScreen.UpdatePoints(points);
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

            return position.GetViewPosition(_pointerPosition);
        }
    }
}
