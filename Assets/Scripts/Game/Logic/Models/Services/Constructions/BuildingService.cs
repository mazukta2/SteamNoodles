﻿using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class BuildingService : IService
    {
        private readonly IRepository<Construction> _constructions;

        public BuildingService(IRepository<Construction> constructions)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
        }

        public Construction Build(ConstructionCard card, FieldPosition position, FieldRotation rotation)
        {
            if (! _constructions.CanPlace(card.Scheme, position, rotation))
                throw new Exception("Can't build construction here");

            var construction = new Construction(card.Scheme, position, rotation);
            var points = _constructions.GetPoints(card.Scheme, position, rotation);

            _constructions.Add(construction);

            _constructions.FireEvent(construction, new ConstructionBuiltByPlayerEvent(points, card));

            return construction;
        }


        public void TryBuild(ConstructionCard card, FieldPosition position, FieldRotation rotation)
        {
            if (_constructions.CanPlace(card.Scheme, position, rotation))
                Build(card, position, rotation);

        }
    }
}
