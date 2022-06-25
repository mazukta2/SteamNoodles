using Game.Assets.Scripts.Game.Logic.Common.Math;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Json.Convertors;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Constructions
{
    public class ConstructionDefinition : IDefinition
    {
        public DefId DefId { get; set; }
        public string Name { get; set; } = "";
        public string LevelViewPath { get; set; }
        public int[,] Placement { get; set; }
        public Requirements Requirements { get; set;  }
        public string HandImagePath { get; set; }
        public int Points { get;  set; }
        [JsonConverter(typeof(DefinitionsDictionaryConventer<ConstructionDefinition, int>))]
        public IReadOnlyDictionary<ConstructionDefinition, int> AdjacencyPoints { get; set; } = new Dictionary<ConstructionDefinition, int>();

        public void Validate()
        {
            if (string.IsNullOrEmpty(LevelViewPath))
                throw new Exception($"{nameof(LevelViewPath)} is empty");

            if (Placement == null || Placement.Length == 0)
                throw new Exception($"{nameof(Placement)} is empty");

            if (string.IsNullOrEmpty(HandImagePath))
                throw new Exception($"{nameof(HandImagePath)} is empty");

            if (string.IsNullOrEmpty(Name))
                throw new Exception($"{nameof(Name)} is empty");
        }
    }

    [Serializable]
    public struct Requirements
    {
        public bool DownEdge;
    }
}

