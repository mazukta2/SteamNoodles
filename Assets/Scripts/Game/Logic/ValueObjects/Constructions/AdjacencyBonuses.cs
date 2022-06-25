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
        private IReadOnlyDictionary<ConstructionScheme, BuildingPoints> _adjacencyPoints;

        public AdjacencyBonuses()
        {
            _adjacencyPoints = new Dictionary<ConstructionScheme, BuildingPoints>();
        }

        public AdjacencyBonuses(IReadOnlyDictionary<ConstructionScheme, BuildingPoints> bonuses)
        {
            _adjacencyPoints = bonuses;
        }

        public int Count => _adjacencyPoints.Count;

        public bool HasAdjacencyBonusWith(ConstructionScheme scheme)
        {
            return _adjacencyPoints.Any(x => x.Key.Id == scheme.Id);
        }

        public BuildingPoints GetAdjacencyBonusWith(ConstructionScheme scheme)
        {
            if (!HasAdjacencyBonusWith(scheme))
                return new BuildingPoints(0);

            return _adjacencyPoints.First(x => x.Key.Id == scheme.Id).Value;
        }

        public IReadOnlyCollection<Tuple<ConstructionScheme, BuildingPoints>> GetAll()
        {
            return _adjacencyPoints.Select(x => new Tuple<ConstructionScheme, BuildingPoints> (x.Key, x.Value)).AsReadOnly();
        }
    }
}
