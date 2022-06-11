using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions
{
    public record ConstructionScheme : Entity
    {
        public LocalizationTag Name { get; }
        public BuildingPoints Points { get;}
        public string HandImagePath { get; }
        public string LevelViewPath { get; }
        public ContructionPlacement Placement { get; }
        public Requirements Requirements { get; }
        public AdjacencyBonuses AdjacencyPoints { get; private set; }
        public float GhostShrinkDistance { get; private set; }
        public float GhostHalfShrinkDistance { get; private set; }

        private DefId _defintionId; // don't expose this field

        public ConstructionScheme(Uid id, 
            ConstructionDefinition definition,
            ConstructionsSettingsDefinition constructionsSettingsDefinition) : base(id)
        {
            Name = new LocalizationTag(definition.Name);
            Points = new BuildingPoints(definition.Points);
            HandImagePath = definition.HandImagePath;
            LevelViewPath = definition.LevelViewPath;
            Placement = new ContructionPlacement(definition.Placement);
            Requirements = definition.Requirements;
            GhostShrinkDistance = constructionsSettingsDefinition.GhostShrinkDistance;
            GhostHalfShrinkDistance = constructionsSettingsDefinition.GhostHalfShrinkDistance;

            _defintionId = definition.DefId;
        }

        public ConstructionScheme(ConstructionDefinition definition, 
            ConstructionsSettingsDefinition constructionsSettingsDefinition)
        {
            Name = new LocalizationTag(definition.Name);
            Points = new BuildingPoints(definition.Points);
            HandImagePath = definition.HandImagePath;
            LevelViewPath = definition.LevelViewPath;
            Placement = new ContructionPlacement(definition.Placement);
            Requirements = definition.Requirements;
            GhostShrinkDistance = constructionsSettingsDefinition.GhostShrinkDistance;
            GhostHalfShrinkDistance = constructionsSettingsDefinition.GhostHalfShrinkDistance;

            _defintionId = definition.DefId;
        }

        public ConstructionScheme(DefId defId = null, ContructionPlacement placement = null,
            LocalizationTag name = null, BuildingPoints points = null,
            AdjacencyBonuses adjacencyPoints = null,
            string image = "", string view = "",
            Requirements requirements = new Requirements(),
            float ghostShrinkDistance = 0, float ghostHalfShrinkDistance = 0)
        {
            Name = name ?? LocalizationTag.None;
            Points = points ?? new BuildingPoints(0);
            AdjacencyPoints = adjacencyPoints ?? new AdjacencyBonuses();
            HandImagePath = image;
            LevelViewPath = view;
            Placement = placement ?? ContructionPlacement.One;
            Requirements = requirements;
            GhostShrinkDistance = ghostShrinkDistance;
            GhostHalfShrinkDistance = ghostHalfShrinkDistance;

            _defintionId = defId ?? DefId.None;
        }

        public ConstructionScheme(Uid id, DefId defId = null, ContructionPlacement placement = null,
            LocalizationTag name = null, BuildingPoints points = null,
            AdjacencyBonuses adjacencyPoints = null,
            string image = "", string view = "",
            Requirements requirements = new Requirements(), 
            float ghostShrinkDistance = 0, float ghostHalfShrinkDistance = 0) : base(id)
        {
            Name = name ?? LocalizationTag.None;
            Points = points ?? new BuildingPoints(0);
            AdjacencyPoints = adjacencyPoints ?? new AdjacencyBonuses();
            HandImagePath = image;
            LevelViewPath = view;
            Placement = placement ?? ContructionPlacement.One;
            Requirements = requirements;
            GhostShrinkDistance = ghostShrinkDistance;
            GhostHalfShrinkDistance = ghostHalfShrinkDistance;

            _defintionId = defId ?? DefId.None;
        }

        public static ConstructionScheme DefaultWithPoints(BuildingPoints points)
        {
            return new ConstructionScheme(new Uid(),
                DefId.None,
                ContructionPlacement.One,
                LocalizationTag.None,
                points,
                new AdjacencyBonuses(),
                "", "", new Requirements());
        }

        public static IReadOnlyCollection<ConstructionScheme> FillWithDefinitions(IEnumerable<ConstructionDefinition> definitions,
            IRepository<ConstructionScheme> repository,
            ConstructionsSettingsDefinition constructionsSettingsDefinition)
        {
            var result = new Dictionary<ConstructionDefinition, ConstructionScheme>();
            foreach (var definition in definitions)
            {
                var entity = new ConstructionScheme(definition, constructionsSettingsDefinition);
                result.Add(definition, entity);
            }

            foreach (var (definition, entity) in result)
            {
                var ad = new AdjacencyBonuses(definition.AdjacencyPoints
                    .ToDictionary(x => Get(x.Key), y => new BuildingPoints(y.Value)).AsReadOnly());
                entity.SetAdjecity(ad);
            }

            foreach (var (definition, entity) in result)
            {
                repository.Add(entity);
            }

            return result.Values.AsReadOnly();

            ConstructionScheme Get(ConstructionDefinition definition)
            {
                return result[definition];
            }
        }

        public void SetAdjecity(AdjacencyBonuses bonuses)
        {
            AdjacencyPoints = bonuses;
        }

        // TODO: maybe we should do this externaly
        public bool IsConnectedToDefinition(ConstructionDefinition definition)
        {
            return _defintionId == definition.DefId;
        }

        public bool IsDownEdge()
        {
            return Requirements.DownEdge;
        }
    }
}
