using System;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostManagerPresenter : BasePresenter<IGhostManagerView>
    {
        private IGhostManagerView _view;
        private readonly BuildingModeService _buildingModeService;

        public GhostManagerPresenter(IGhostManagerView view) 
            : this(view,
                  IPresenterServices.Default?.Get<BuildingModeService>())
        {
        }

        public GhostManagerPresenter(IGhostManagerView view, BuildingModeService buildingModeService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
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
            //var view = _view.Container.Spawn<IGhostView>(_view.GhostPrototype);
            //var ghost = new GhostPresenter(_settings, _screenManager, _fieldService, _buildingService, buildScreen,
            //    _controls, IGameKeysManager.Default, IGameAssets.Default, view, _time);
            //_ghost.OnGhostPostionChanged += UpdateGhostPosition;
            //_commands.Execute(new AddGhostCommand(_buildingModeService.Card, _view.Container, _view.GhostPrototype));
        }

        private void RemoveGhost()
        {
            //_container.Clear();
        }

    }
}
