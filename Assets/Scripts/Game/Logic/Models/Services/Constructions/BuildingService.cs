using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class BuildingService : IService
    {
        private readonly IRepository<Construction> _constructions;
        private readonly ConstructionsService _constructionsService;
        private readonly HandService _handService;

        public BuildingService(IRepository<Construction> constructions, ConstructionsService constructionsService,
             HandService handService)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _constructionsService = constructionsService;
            _handService = handService ?? throw new ArgumentNullException(nameof(handService));
        }

        public Construction Build(ConstructionCard card, FieldPosition position, FieldRotation rotation)
        {
            if (!_constructionsService.CanPlace(card, position, rotation))
                throw new Exception("Can't build construction here");

            if (!_handService.Has(card))
                throw new Exception("No such card in hand");

            var construction = new Construction(card.Scheme, position, rotation);
            var points = _constructionsService.GetPoints(card, position, rotation);

            _handService.Remove(card);
            _constructions.Add(construction);

            _constructions.FireEvent(construction, new ConstructionBuiltByPlayerEvent(points));

            return construction;
        }


    }
}
