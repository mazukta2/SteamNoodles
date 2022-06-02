using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class GhostPresenter : BasePresenter<IGhostView>
    {
        //public event Action OnGhostPostionChanged = delegate { };
        //public ConstructionScheme Scheme { get; private set; }
        //public FieldRotation Rotation { get; private set; }

        private readonly IGhostView _view;
        private readonly BuildingModeService _buildingModeService;
        private readonly FieldService _fieldService;
        private readonly IControls _controls;
        private readonly IPresenterCommands _commands;

        private GameVector3 _pointerPosition;

        //private readonly IGameTime _time;
        //private readonly IGameKeysManager _gameKeysManager;
        //private readonly ScreenManagerPresenter _screenManager;
        //private GameVector3 _pointerPosition;
        //private KeyCommand _rotateLeft;
        //private KeyCommand _rotateRight;
        //private IConstructionModelView _modelView;

        public GhostPresenter(IGhostView view) : this(view,
            IPresenterServices.Default.Get<BuildingModeService>(),
            IPresenterServices.Default.Get<FieldService>(),
            IGameControls.Default,
            IPresenterCommands.Default)
        {

        }

        public GhostPresenter(IGhostView view,
            BuildingModeService buildingModeService,
            FieldService fieldService,
            IGameControls controls, IPresenterCommands commands) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _controls = controls ?? throw new ArgumentNullException(nameof(controls));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));

            if (!_buildingModeService.IsEnabled) throw new Exception("Ghost can exist only in building mode");


            //_time = time;
            //_constructionsSettings = constructionsSettings ?? throw new ArgumentNullException(nameof(constructionsSettings));
            //_gameKeysManager = gameKeysManager ?? throw new ArgumentNullException(nameof(gameKeysManager));
            //_buildScreen = buildScreen ?? throw new ArgumentNullException(nameof(buildScreen));
            ////_buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
            //_screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            //Scheme = _buildScreen.CurrentCard.Scheme ?? throw new ArgumentNullException(nameof(_buildScreen.CurrentCard.Scheme));
            //Rotation = new FieldRotation(FieldRotation.Rotation.Top);

            //_rotateLeft = _gameKeysManager.GetKey(GameKeys.RotateLeft);
            //_rotateRight = _gameKeysManager.GetKey(GameKeys.RotateRight);

            _commands.Execute(new AddGhostModelCommand(_buildingModeService.Card.Scheme, _view.Container));

            //_rotateLeft.OnTap += HandleRotateLeftTap;
            //_rotateRight.OnTap += HandleRotateRightTap;
            _buildingModeService.OnChanged += HandleModeOnChanged;
            _buildingModeService.OnPositionChanged += HandleOnPositionChanged;
            _controls.OnLevelClick += HandleOnLevelClick;
            _controls.OnLevelPointerMoved += HandleOnPointerMoved;


            HandleOnPointerMoved(_controls.PointerLevelPosition);
        }

        //public GhostPresenter(ConstructionsSettingsDefinition constructionsSettings, 
        //    ScreenManagerPresenter screenManager,
        //    FieldService fieldService,
        //    BuildScreenPresenter buildScreen, IControls controls, IGameKeysManager gameKeysManager, IAssets assets, IGhostView view, IGameTime time) : base(view)
        //{
        //    _view = view ?? throw new ArgumentNullException(nameof(view));
        //    _time = time;
        //    _constructionsSettings = constructionsSettings ?? throw new ArgumentNullException(nameof(constructionsSettings));
        //    _controls = controls ?? throw new ArgumentNullException(nameof(controls));
        //    _gameKeysManager = gameKeysManager ?? throw new ArgumentNullException(nameof(gameKeysManager));
        //    _buildScreen = buildScreen ?? throw new ArgumentNullException(nameof(buildScreen));
        //    //_buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
        //    _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
        //    _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
        //    _assets = assets ?? throw new ArgumentNullException(nameof(assets));
        //    Scheme = _buildScreen.CurrentCard.Scheme ?? throw new ArgumentNullException(nameof(_buildScreen.CurrentCard.Scheme));
        //    Rotation = new FieldRotation(FieldRotation.Rotation.Top);

        //    _rotateLeft = _gameKeysManager.GetKey(GameKeys.RotateLeft);
        //    _rotateRight = _gameKeysManager.GetKey(GameKeys.RotateRight);

        //    _view.Container.Clear();
        //    _modelView = _view.Container.Spawn<IConstructionModelView>(_assets.GetPrefab(_buildScreen.CurrentCard.Scheme.LevelViewPath));
      

        //    _rotateLeft.OnTap += HandleRotateLeftTap;
        //    _rotateRight.OnTap += HandleRotateRightTap;

        //    _controls.OnLevelClick += HandleOnLevelClick;
        //    _controls.OnLevelPointerMoved += HandleOnPointerMoved;

        //    HandleOnPointerMoved(_controls.PointerLevelPosition);
        //}

        protected override void DisposeInner()
        {
            _controls.OnLevelClick -= HandleOnLevelClick;
            _controls.OnLevelPointerMoved -= HandleOnPointerMoved;
            _buildingModeService.OnChanged -= HandleModeOnChanged;
            _buildingModeService.OnPositionChanged -= HandleOnPositionChanged;

            //_rotateLeft.OnTap -= HandleRotateLeftTap;
            //_rotateRight.OnTap -= HandleRotateRightTap;
        }

        private void HandleModeOnChanged(bool value)
        {
            if (!value) _view.Dispose();
        }

        //public int GetPointChanges()
        //{
        //    return _buildingService.GetPoints(_buildScreen.CurrentCard, GetGridPosition(), Rotation).Value;
        //}

        private void HandleOnLevelClick()
        {
            //if (_buildingService.CanPlace(_buildScreen.CurrentCard, GetGridPosition(), Rotation))
            //{
            //    _buildingService.Build(_buildScreen.CurrentCard, GetGridPosition(), Rotation);
            //}
            //_screenManager.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

        private void HandleOnPointerMoved(GameVector3 worldPosition)
        {
            _pointerPosition = worldPosition;
            HandlePointerPositionUpdated();
        }

        //private bool CanPlace()
        //{
        //    //if (_buildingService.CanPlace(_buildScreen.CurrentCard, GetGridPosition(), Rotation))
        //    //{
        //    //    return true;
        //    //}
        //    return false;
        //}

        //private void UpdatePoints()
        //{
        //    //var points = 0;
        //    //points += _buildingService.GetPoints(_buildScreen.CurrentCard, GetGridPosition(), Rotation).Value;

        //    //_buildScreen.UpdatePoints(GetWorldPosition(), points,
        //    //    _buildingService.GetAdjacencyPoints(_buildScreen.CurrentCard, GetGridPosition(), Rotation));
        //}


        //private void HandleRotateLeftTap()
        //{
        //    //Rotation = FieldRotation.RotateLeft(Rotation);
        //    //UpdatePosition();
        //}

        //private void HandleRotateRightTap()
        //{
        //    //Rotation = FieldRotation.RotateRight(Rotation);
        //    //UpdatePosition();
        //}

        //private FieldPosition GetGridPosition()
        //{
        //    return _fieldService.GetWorldConstructionToField(_pointerPosition, GetSize());
        //}

        //private GameVector3 GetWorldPosition()
        //{
        //    return _fieldService.GetAlignWithAGrid(_pointerPosition, GetSize());
        //}

        //private IntRect GetSize()
        //{
        //    return _card.Get().Scheme.Placement.GetRect(_buildingModeService.GetRotation());
        //}

        //public GameVector3 GetTargetPosition()
        //{
        //    return _pointerPosition;
        //}

        private void HandlePointerPositionUpdated()
        {
            var size = _buildingModeService.Card.Scheme.Placement.GetRect(_buildingModeService.GetRotation());
            var fieldPosition = _fieldService.GetWorldConstructionToField(_pointerPosition, size);
            _buildingModeService.SetGhostPosition(fieldPosition, _buildingModeService.GetRotation());

            //UpdatePoints();
            //OnGhostPostionChanged();
        }
        private void HandleOnPositionChanged()
        {
            var size = _buildingModeService.Card.Scheme.Placement.GetRect(_buildingModeService.GetRotation());
            var worldPosition = _fieldService.GetWorldPosition(_buildingModeService.GetPosition(), size);
            _view.LocalPosition.Value = worldPosition;
            _view.Rotator.Rotation = FieldRotation.ToDirection(_buildingModeService.GetRotation());
        }

    }
}
