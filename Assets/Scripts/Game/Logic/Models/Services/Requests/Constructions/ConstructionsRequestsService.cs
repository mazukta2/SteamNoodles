using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Requests
{
    public class ConstructionsRequestsService : IService
    {
        public event Action OnUpdate = delegate { };

        private readonly FieldService _fieldService;
        private readonly ICommands _commands;
        private readonly IGameAssets _assets;
        private readonly IRepository<Construction> _constructions;
        private readonly BuildingModeService _buildingModeService;

        public ConstructionsRequestsService(IRepository<Construction> constructions, 
            BuildingModeService buildingModeService,
            FieldService fieldService, ICommands commands, IGameAssets assets)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
        }

        public ConstructionModel Get(Uid id)
        {
            var construction = _constructions.Get(id);
            var worldPosition = _fieldService.GetWorldPosition(construction);
            return new ConstructionModel(construction, worldPosition, this, _assets);
        }

        public float GetShrink(ConstructionModel model)
        {
            if (_buildingModeService.IsEnabled)
            {
                var distance = _buildingModeService.GetTargetPosition().GetDistanceTo(model.WorldPosition);
                if (distance > model.GhostShrinkDistance)
                    return 1;
                else if (distance > model.GhostHalfShrinkDistance)
                    return distance / model.GhostShrinkDistance;
                else
                    return 0.2f;
            }
            else
            {
                return 1;
            }
        }
    }
}
