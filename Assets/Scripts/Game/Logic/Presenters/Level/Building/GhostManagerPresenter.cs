using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level
{
    public class GhostManagerPresenter : BasePresenter<IGhostManagerView>
    {
        private IGhostManagerView _view;
        private readonly BuildingModeService _buildingModeService;
        private readonly IPresenterCommands _commands;

        public GhostManagerPresenter(IGhostManagerView view) 
            : this(view,
                  IPresenterServices.Default?.Get<BuildingModeService>(),
                  IPresenterCommands.Default)
        {
        }

        public GhostManagerPresenter(IGhostManagerView view, BuildingModeService buildingModeService, IPresenterCommands commands) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
            _buildingModeService.OnChanged += HandleBuildingModeChanged;
        }

        protected override void DisposeInner()
        {
            _buildingModeService.OnChanged -= HandleBuildingModeChanged;
            RemoveGhost();
        }

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
