using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions
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
