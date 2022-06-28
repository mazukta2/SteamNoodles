using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions
{
    public record AdjacencyBonuses
    {
        private IReadOnlyDictionary<ConstructionSchemeEntity, BuildingPoints> _adjacencyPoints;

        public AdjacencyBonuses()
        {
            _adjacencyPoints = new Dictionary<ConstructionSchemeEntity, BuildingPoints>();
        }

        public AdjacencyBonuses(IReadOnlyDictionary<ConstructionSchemeEntity, BuildingPoints> bonuses)
        {
            _adjacencyPoints = bonuses;
        }

        public int Count => _adjacencyPoints.Count;

        public bool HasAdjacencyBonusWith(ConstructionSchemeEntity schemeEntity)
        {
            return _adjacencyPoints.Any(x => x.Key.Id == schemeEntity.Id);
        }

        public BuildingPoints GetAdjacencyBonusWith(ConstructionSchemeEntity schemeEntity)
        {
            if (!HasAdjacencyBonusWith(schemeEntity))
                return new BuildingPoints(0);

            return _adjacencyPoints.First(x => x.Key.Id == schemeEntity.Id).Value;
        }

        public IReadOnlyCollection<Tuple<ConstructionSchemeEntity, BuildingPoints>> GetAll()
        {
            return _adjacencyPoints.Select(x => new Tuple<ConstructionSchemeEntity, BuildingPoints> (x.Key, x.Value)).AsReadOnly();
        }
    }
}
