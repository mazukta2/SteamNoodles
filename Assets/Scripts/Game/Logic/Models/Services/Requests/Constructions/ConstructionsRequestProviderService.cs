using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Requests
{
    public class ConstructionsRequestProviderService : IService
    {
        public event Action OnUpdate = delegate { };

        private readonly FieldService _fieldService;
        private readonly ICommands _commands;
        private readonly IRepository<Construction> _constructions;

        public ConstructionsRequestProviderService(IRepository<Construction> constructions, 
            FieldService fieldService, ICommands commands)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        public ConstructionModel Get(Uid id)
        {
            var construction = _constructions.Get(id);
            var worldPosition = _fieldService.GetWorldPosition(construction);
            return new ConstructionModel(construction, worldPosition, this);
        }
    }
}
