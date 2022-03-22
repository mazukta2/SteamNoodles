using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Math;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public class LevelDefinition
    {
        public string SceneName { get; set; }

        [JsonConverter(typeof(DefinitionsConventer<ConstructionDefinition>))]
        public IReadOnlyCollection<ConstructionDefinition> StartingHand { get; set; } = new List<ConstructionDefinition>();
        public int HandSize { get; set; }

        public IReadOnlyCollection<PlacementFieldDefinition> PlacementFields { get; set; } = new List<PlacementFieldDefinition>();


        public int CrowdUnitsAmount { get; set; }

        [JsonConverter(typeof(DefinitionsDictionaryConventer<CustomerDefinition, int>))]
        public IReadOnlyDictionary<CustomerDefinition, int> BaseCrowdUnits { get; set; } = new Dictionary<CustomerDefinition, int>();
        public FloatRect UnitsRect { get; set; }
    }
}
