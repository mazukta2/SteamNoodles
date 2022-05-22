using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class GhostPresenter : BasePresenter<IGhostView>
    {
        public event Action OnGhostPostionChanged = delegate { };
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
        private GameVector3 _pointerPosition;
        private KeyCommand _rotateLeft;
        private KeyCommand _rotateRight;
        private IConstructionModelView _modelView;
        private FieldPositionsCalculator _positionCalculator;

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

            _positionCalculator = new FieldPositionsCalculator(_constructionsSettings.CellSize);

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

        public IntPoint GetGridPosition()
        {
            return _positionCalculator.GetGridPositionByMapPosition(_pointerPosition, GetSize());
        }

        public GameVector3 GetWorldPosition()
        {
            return _positionCalculator.GetAlignWithAGrid(_pointerPosition, GetSize());
        }

        public IntRect GetSize()
        {
            return _buildScreen.CurrentCard.Definition.GetRect(Rotation);
        }

        public GameVector3 GetTargetPosition()
        {
            return _pointerPosition;
        }

        public int GetPointChanges()
        {
            return _constructionsManager.GetPoints(_buildScreen.CurrentCard.Definition, GetGridPosition(), Rotation);
        }

        private void HandleOnLevelClick()
        {
            if (_constructionsManager.CanPlace(_buildScreen.CurrentCard, GetGridPosition(), Rotation))
            {
                _constructionsManager.Build(_buildScreen.CurrentCard, GetGridPosition(), Rotation);
            }
            _screenManager.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

        private void HandleOnPointerMoved(GameVector3 worldPosition)
        {
            _pointerPosition = worldPosition;
            UpdatePosition();
        }

        private bool CanPlace()
    {
            if (_constructionsManager.CanPlace(_buildScreen.CurrentCard, GetGridPosition(), Rotation))
            {
                return true;
            }
            return false;
        }

        private void UpdatePoints()
        {
            var points = 0;
            points += _constructionsManager.GetPoints(_buildScreen.CurrentCard.Definition, GetGridPosition(), Rotation);

            _buildScreen.UpdatePoints(GetWorldPosition(), points, 
                _constructionsManager
                .GetAdjacencyPoints(_buildScreen.CurrentCard.Definition, GetGridPosition(), Rotation));
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
            _view.Rotator.Rotation = ConstructionRotation.ToDirection(Rotation);
            _view.LocalPosition.Value = GetWorldPosition();
            UpdatePoints();
            OnGhostPostionChanged();
        }
    }
}
