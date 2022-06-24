using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost
{
    public class GhostBuildingService : Disposable, IService
    {
        private readonly GameControlsService _controlsService;
        private readonly ISingletonRepository<ConstructionGhost> _ghost;
        private readonly BuildingService _buildingService;
        private readonly ConstructionsService _constructionsService;
        
        public GhostBuildingService(
            ISingletonRepository<ConstructionGhost> ghost,
            ConstructionsService constructionsService, 
            BuildingService buildingService, 
            GameControlsService controlsService)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
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
            if (!_ghost.Has())
                return;
            
            var ghost = _ghost.Get();
            
            if (_constructionsService.CanPlace(ghost.Card, ghost.Position, ghost.Rotation))
                _buildingService.Build(ghost.Card, ghost.Position, ghost.Rotation);
            
            _ghost.Remove();
        }
    }
}
