using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class BuildingService 
    {
        private readonly IRepository<Construction> _constructions;
        private readonly ConstructionsService _constructionsService;
        private readonly BuildingPointsService _pointsService;
        private readonly HandService _handService;
        private readonly FieldService _fieldService;

        public BuildingService(IRepository<Construction> constructions, ConstructionsService constructionsService,
            BuildingPointsService pointsService, HandService handService, FieldService fieldService)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _constructionsService = constructionsService;
            _pointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
            _handService = handService ?? throw new ArgumentNullException(nameof(handService));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
        }

        public Construction Build(ConstructionCard card, FieldPosition position, FieldRotation rotation)
        {
            if (!_constructionsService.CanPlace(card, position, rotation))
                throw new Exception("Can't build construction here");

            var construction = new Construction(card.Scheme, position, rotation);
            var points = _constructionsService.GetPoints(card, position, rotation);

            _handService.Remove(card);
            _constructions.Add(construction);

            _pointsService.ChangePoints(points, _fieldService.GetWorldPosition(construction));

            _constructions.FireEvent(construction, new ConstructionBuildedByPlayerEvent());

            return construction;
        }


    }
}
