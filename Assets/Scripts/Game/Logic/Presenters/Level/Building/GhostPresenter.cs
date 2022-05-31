using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
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
        public ConstructionScheme Scheme { get; private set; }
        public FieldRotation Rotation { get; private set; }

        private readonly IGhostView _view;
        private readonly IGameTime _time;
        private readonly ConstructionsSettingsDefinition _constructionsSettings;
        private readonly IControls _controls;
        private readonly IGameKeysManager _gameKeysManager;
        private readonly BuildScreenPresenter _buildScreen;
        private readonly IBuildingPresenterService _buildingService;
        private readonly ScreenManagerPresenter _screenManager;
        private readonly IFieldPresenterService _fieldService;
        private readonly IAssets _assets;
        private GameVector3 _pointerPosition;
        private KeyCommand _rotateLeft;
        private KeyCommand _rotateRight;
        private IConstructionModelView _modelView;

        public GhostPresenter(ConstructionsSettingsDefinition constructionsSettings, 
            ScreenManagerPresenter screenManager,
            IFieldPresenterService fieldService,
            IBuildingPresenterService buildingService,
            BuildScreenPresenter buildScreen, IControls controls, IGameKeysManager gameKeysManager, IAssets assets, IGhostView view, IGameTime time) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _time = time;
            _constructionsSettings = constructionsSettings ?? throw new ArgumentNullException(nameof(constructionsSettings));
            _controls = controls ?? throw new ArgumentNullException(nameof(controls));
            _gameKeysManager = gameKeysManager ?? throw new ArgumentNullException(nameof(gameKeysManager));
            _buildScreen = buildScreen ?? throw new ArgumentNullException(nameof(buildScreen));
            _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            Scheme = _buildScreen.CurrentCard.Scheme ?? throw new ArgumentNullException(nameof(_buildScreen.CurrentCard.Scheme));
            Rotation = new FieldRotation(FieldRotation.Rotation.Top);

            _rotateLeft = _gameKeysManager.GetKey(GameKeys.RotateLeft);
            _rotateRight = _gameKeysManager.GetKey(GameKeys.RotateRight);

            _view.Container.Clear();
            _modelView = _view.Container.Spawn<IConstructionModelView>(_assets.GetPrefab(_buildScreen.CurrentCard.Scheme.Definition.LevelViewPath));
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

        public FieldPosition GetGridPosition()
        {
            return _fieldService.GetWorldConstructionToField(_pointerPosition, GetSize());
        }

        public GameVector3 GetWorldPosition()
        {
            return _fieldService.GetAlignWithAGrid(_pointerPosition, GetSize());
        }

        public IntRect GetSize()
        {
            return _buildScreen.CurrentCard.Scheme.Definition.GetRect(Rotation);
        }

        public GameVector3 GetTargetPosition()
        {
            return _pointerPosition;
        }

        public int GetPointChanges()
        {
            return _buildingService.GetPoints(_buildScreen.CurrentCard, GetGridPosition(), Rotation).Value;
        }

        private void HandleOnLevelClick()
        {
            if (_buildingService.CanPlace(_buildScreen.CurrentCard, GetGridPosition(), Rotation))
            {
                _buildingService.Build(_buildScreen.CurrentCard, GetGridPosition(), Rotation);
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
            if (_buildingService.CanPlace(_buildScreen.CurrentCard, GetGridPosition(), Rotation))
            {
                return true;
            }
            return false;
        }

        private void UpdatePoints()
        {
            var points = 0;
            points += _buildingService.GetPoints(_buildScreen.CurrentCard, GetGridPosition(), Rotation).Value;

            _buildScreen.UpdatePoints(GetWorldPosition(), points,
                _buildingService.GetAdjacencyPoints(_buildScreen.CurrentCard, GetGridPosition(), Rotation));
        }


        private void HandleRotateLeftTap()
        {
            Rotation = FieldRotation.RotateLeft(Rotation);
            UpdatePosition();
        }

        private void HandleRotateRightTap()
        {
            Rotation = FieldRotation.RotateRight(Rotation);
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            _modelView.BorderAnimator.Play(CanPlace() ? IConstructionModelView.BorderAnimations.Idle.ToString() : IConstructionModelView.BorderAnimations.Disallowed.ToString());
            _view.Rotator.Rotation = FieldRotation.ToDirection(Rotation);
            _view.LocalPosition.Value = GetWorldPosition();
            UpdatePoints();
            OnGhostPostionChanged();
        }
    }
}
