using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level
{
    public class GhostManagerPresenter : BasePresenter<IGhostManagerView>
    {
        //public event Action OnGhostChanged = delegate { };
        //public event Action OnGhostPostionChanged = delegate { };

        private IGhostManagerView _view;
        private readonly BuildingModeService _buildingModeService;
        private readonly IPresenterCommands _commands;
        private readonly IGameTime _time;
        private ScreenManagerPresenter _screenManager;
        private readonly FieldService _fieldService;
        private ConstructionsSettingsDefinition _settings;
        private IControls _controls;
        private GhostPresenter _ghost;

        public GhostManagerPresenter(IGhostManagerView view) 
            : this(view,
                  IPresenterServices.Default?.Get<BuildingModeService>(),
                  IPresenterCommands.Default)
        {
            //ScreenManagerPresenter.Default,
            //    IGameDefinitions.Default.Get<ConstructionsSettingsDefinition>(), 
            //    IGameControls.Default,
            //    IStageLevelService.Default.Building, IStageLevelService.Default.Field, this, IGameTime.Default
        }

        public GhostManagerPresenter(IGhostManagerView view, BuildingModeService buildingModeService, IPresenterCommands commands) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
            _buildingModeService.OnChanged += HandleBuildingModeChanged;
        }

        //public GhostManagerPresenter(ScreenManagerPresenter screenManager, ConstructionsSettingsDefinition settings, IControls controls,
        //    IBuildingPresenterService buildingService,
        //    FieldService fieldService,
        //    IGhostManagerView view, IGameTime time) : base(view)
        //{
        //    _view = view ?? throw new ArgumentNullException(nameof(view));
        //    _time = time;
        //    _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
        //    _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
        //    _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
        //    _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        //    _controls = controls ?? throw new ArgumentNullException(nameof(controls));

        //}

        protected override void DisposeInner()
        {
            _buildingModeService.OnChanged -= HandleBuildingModeChanged;
            RemoveGhost();
        }

        //public GhostPresenter GetGhost()
        //{
        //    return _ghost;
        //}

        //public int GetPointChanges()
        //{
        //    var ghost = GetGhost();
        //    if (ghost != null)
        //        return ghost.GetPointChanges();
        //    else
        //        return 0;
        //}

        //private void UpdateGhostPosition()
        //{
        //    OnGhostPostionChanged();
        //}

        private void HandleBuildingModeChanged(bool value)
        {
            if (value)
                CreateGhost();
            else
                RemoveGhost();
        }

        private void CreateGhost()
        {
            _commands.Execute(new AddGhostCommand(_buildingModeService.Card, _view.Container, _view.GhostPrototype));
        }

        private void RemoveGhost()
        {
            _commands.Execute(new RemoveGhostCommand(_view.Container));
        }

    }
}
