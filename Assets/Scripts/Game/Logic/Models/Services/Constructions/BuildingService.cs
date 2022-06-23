using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class BuildingService : IService
    {
        private readonly IRepository<Construction> _constructions;
        private readonly ConstructionsService _constructionsService;

        public BuildingService(IRepository<Construction> constructions, ConstructionsService constructionsService)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _constructionsService = constructionsService;
        }

        public Construction Build(ConstructionCard card, CellPosition position, FieldRotation rotation)
        {
            if (!_constructionsService.CanPlace(card, position, rotation))
                throw new Exception("Can't build construction here");

            var construction = new Construction(card.Scheme, position, rotation);
            var points = _constructionsService.GetPoints(card, position, rotation);

            _constructions.Add(construction);

            _constructions.FireEvent(construction, new ConstructionBuiltByPlayerEvent(points, card));

            return construction;
        }


    }
}
