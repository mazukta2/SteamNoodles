using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Services.Fields;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions.Ghost
{
    public class GhostBuildingService : Disposable, IService
    {
        private readonly GameControlsService _controlsService;
        private readonly ISingletonRepository<ConstructionGhost> _ghost;
        private readonly IDataProvider<GhostData> _ghostData;
        private readonly BuildingService _buildingService;
        private readonly BuildingAggregatorService _statusAggregatorService;

        public GhostBuildingService(
            ISingletonRepository<ConstructionGhost> ghost,
            IDataProvider<GhostData> ghostData,
            BuildingService buildingService,
            GameControlsService controlsService)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _ghostData = ghostData ?? throw new ArgumentNullException(nameof(ghostData));
            _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
            _controlsService = controlsService ?? throw new ArgumentNullException(nameof(controlsService));
            _controlsService.OnLevelClick += HandleOnLevelClick;
        }

        protected override void DisposeInner()
        {
            _controlsService.OnLevelClick -= HandleOnLevelClick;
        }

        private void HandleOnLevelClick()
        {
            if (!_ghostData.Has())
                return;
            
            _buildingService.TryBuild(_ghostData.Get());
            
            _ghost.Remove();
        }
    }
}
