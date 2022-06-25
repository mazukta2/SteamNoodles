using System;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions
{
    public class BuildingService : IService
    {
        private readonly IRepository<Construction> _constructions;

        public BuildingService(IRepository<Construction> constructions)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
        }

        public Construction Build(GhostData ghost)
        {
            if (!ghost.CanBuild)
                throw new Exception("Can't build construction here");

            var construction = new Construction(ghost.Card.Scheme, ghost.Position, ghost.Rotation);
            var points = ghost.Points;

            _constructions.Add(construction);

            _constructions.FireEvent(construction, new ConstructionBuiltByPlayerEvent(points, ghost.Card));

            return construction;
        }
        
        public Construction Build(ConstructionCard card, FieldPosition position, FieldRotation rotation)
        {
            var ghost = new GhostData();
            ghost.Card = card;
            ghost.Position = position;
            ghost.Rotation = rotation;
            ghost.CanBuild = true;
            return Build(ghost);
        }

        public void TryBuild(GhostData ghost)
        {
            if (ghost.CanBuild)
                Build(ghost);

        }
    }
}
