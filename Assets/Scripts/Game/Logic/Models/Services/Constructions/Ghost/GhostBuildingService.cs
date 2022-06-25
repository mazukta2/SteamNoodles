using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Models.Services.Fields;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost
{
    public class GhostBuildingService : Disposable, IService
    {
        private readonly GameControlsService _controlsService;
        private readonly ISingletonRepository<ConstructionGhost> _ghost;
        private readonly BuildingService _buildingService;
        private readonly FieldCellsService _cellsService;

        public GhostBuildingService(
            ISingletonRepository<ConstructionGhost> ghost,
            BuildingService buildingService,
            GameControlsService controlsService)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
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
            
            _buildingService.TryBuild(ghost.Card, ghost.Position, ghost.Rotation);
            
            _ghost.Remove();
        }
    }
}
