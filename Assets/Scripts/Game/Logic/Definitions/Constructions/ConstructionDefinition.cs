using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Constructions
{
    public class ConstructionDefinition
    {
        public string LevelViewPath { get; set; }
        public IntPoint Size { get; set;  }
        public Requirements Requirements { get; set;  }
        //public ISprite HandIcon { get; }
        //public IVisual BuildingView { get; }
        //public IReadOnlyCollection<IConstructionFeatureSettings> Features { get; }
        //public IReadOnlyDictionary<ConstructionTag, int> Tags { get; }

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

