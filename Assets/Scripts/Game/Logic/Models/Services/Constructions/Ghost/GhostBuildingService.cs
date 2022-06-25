using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost
{
    public class GhostBuildingService : Disposable, IService
    {
        private readonly GameControlsService _controlsService;
        private readonly ISingletonRepository<ConstructionGhost> _ghost;
        private readonly IEntityList<Construction> _constructions;
        private readonly BuildingService _buildingService;
        
        public GhostBuildingService(
            ISingletonRepository<ConstructionGhost> ghost,
            IEntityList<Construction> constructions,
            BuildingService buildingService, 
            GameControlsService controlsService)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
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
            if (!_ghost.Has())
                return;
            
            var ghost = _ghost.Get();
            
            if (_constructions.CanPlace(ghost.Card, ghost.Position, ghost.Rotation))
                _buildingService.Build(ghost.Card, ghost.Position, ghost.Rotation);
            
            _ghost.Remove();
        }
    }
}
