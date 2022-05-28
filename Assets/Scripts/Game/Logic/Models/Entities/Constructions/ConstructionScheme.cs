using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions
{
    public record ConstructionScheme : Entity
    {
        public ConstructionDefinition Definition { get; }

        public LocalizationTag Name { get; }
        public BuildingPoints Points { get;}
        public string HandImagePath { get; }
        public AdjacencyBonuses AdjacencyPoints { get; private set; }

        public ConstructionScheme(Uid id, ConstructionDefinition definition, LocalizationTag name, BuildingPoints points,
            AdjacencyBonuses adjacencyPoints, string image) : base(id)
        {
            Definition = definition;
            Name = name;
            Points = points;
            AdjacencyPoints = adjacencyPoints;
            HandImagePath = image;
        }

        public static IReadOnlyCollection<ConstructionScheme> FillWithDefinitions(IEnumerable<ConstructionDefinition> definitions, IRepository<ConstructionScheme> repository)
        {
            var result = new List<ConstructionScheme>();
            foreach (var definition in definitions)
            {
                var entity = new ConstructionScheme(definition);
                result.Add(entity);
            }

            foreach (var entity in result)
            {
                var definition = entity.Definition;
                entity.AdjacencyPoints = new AdjacencyBonuses(definition.AdjacencyPoints.ToDictionary(x => Get(x.Key), y => new BuildingPoints(y.Value)).AsReadOnly());
            }

            foreach (var entity in result)
            {
                repository.Add(entity);
            }

            return result;

            ConstructionScheme Get(ConstructionDefinition definition)
            {
                var value = repository.Get().FirstOrDefault(x => x.Definition == definition);
                if (value != null)
                    return value;

                value = result.FirstOrDefault(x => x.Definition == definition);
                if (value != null)
                    return value;

                throw new Exception("Can't find entity with that definition");
            }
        }

        private ConstructionScheme(ConstructionDefinition definition) 
        {
            Definition = definition;
            Name = new LocalizationTag(definition.Name);
            Points = new BuildingPoints(definition.Points);
            HandImagePath = definition.HandImagePath;

        }

        public IReadOnlyCollection<FieldPosition> GetOccupiedSpace(FieldPosition position, FieldRotation rotation)
        {
            return Definition.GetOccupiedSpace(position.Value, rotation).Select(x => new FieldPosition(x)).AsReadOnly();
        }

        public bool IsDownEdge()
        {
            return Definition.Requirements.DownEdge;
        }

        public int GetHeight(FieldRotation rotation)
        {
            return Definition.GetRect(rotation).Height;
        }

    }
}
