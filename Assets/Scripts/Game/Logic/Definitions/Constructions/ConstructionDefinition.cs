using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Constructions
{
    public class ConstructionDefinition
    {
        public string LevelViewPath { get; set; }
        public IntPoint Size { get; set;  }
        public Requirements Requirements { get; set;  }
        public string HandImagePath { get; set; }
        public int Points { get;  set; }
        [JsonConverter(typeof(DefinitionsDictionaryConventer<ConstructionDefinition, int>))]
        public Dictionary<ConstructionDefinition, int> AdjacencyPoints { get; set; } = new Dictionary<ConstructionDefinition, int>();

        public IReadOnlyCollection<IntPoint> GetOccupiedSpace(IntPoint position)
        {
            var result = new List<IntPoint>();
            for (int x = 0; x < Size.X; x++)
            {
                for (int y = 0; y < Size.Y; y++)
                {
                    result.Add(position + new IntPoint(x, y));
                }
            }
            return result.AsReadOnly();
        }
    }

    [Serializable]
    public struct Requirements
    {
        public bool DownEdge;
    }
}

