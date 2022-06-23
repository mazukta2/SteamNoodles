using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions
{
    public class GhostBuildingService : Disposable, IService
    {
        private readonly GameControlsService _controlsService;
        private readonly GhostService _ghostService;
        private readonly BuildingService _buildingService;
        private readonly ConstructionsService _constructionsService;
        
        public GhostBuildingService() : this(
            IPresenterServices.Default.Get<GhostService>(),
            IPresenterServices.Default.Get<ConstructionsService>(),
            IPresenterServices.Default.Get<BuildingService>(),
            IPresenterServices.Default.Get<GameControlsService>()
            )
        {
            
        }
        
        public GhostBuildingService(
            GhostService ghostService,
            ConstructionsService constructionsService, 
            BuildingService buildingService, 
            GameControlsService controlsService)
        {
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _controlsService = controlsService ?? throw new ArgumentNullException(nameof(controlsService));
            _controlsService.OnLevelClick += HandleOnLevelClick;
        }

        protected override void DisposeInner()
        {
            _controlsService.OnLevelClick -= HandleOnLevelClick;
        }

        private void HandleOnLevelClick()
        {
            if (!_ghostService.IsEnabled())
                return;
            
            var ghost = _ghostService.GetGhost();
            
            if (_constructionsService.CanPlace(ghost.Card, ghost.Position, ghost.Rotation))
                _buildingService.Build(ghost.Card, ghost.Position, ghost.Rotation);
            // _screenService.Open<IMainScreenView>(x => x.Init());
        }
    }
}
