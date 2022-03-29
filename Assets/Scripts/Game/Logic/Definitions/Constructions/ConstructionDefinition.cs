using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Constructions
{
    public class ConstructionDefinition
    {
        public string LevelViewPath { get; set; }
        public int[,] Placement { get; set; }
        public Requirements Requirements { get; set;  }
        public string HandImagePath { get; set; }
        public int Points { get;  set; }
        [JsonConverter(typeof(DefinitionsDictionaryConventer<ConstructionDefinition, int>))]
        public Dictionary<ConstructionDefinition, int> AdjacencyPoints { get; set; } = new Dictionary<ConstructionDefinition, int>();

        public IReadOnlyCollection<IntPoint> GetOccupiedSpace(IntPoint position)
        {
            var result = new List<IntPoint>();

            var occupied = new List<IntPoint>();
            for (int x = 0; x < Placement.GetLength(0); x++)
            {
                for (int y = 0; y < Placement.GetLength(1); y++)
                {
                    if (Placement[x, y] != 0)
                        occupied.Add(new IntPoint(x, y));
                }
            }

            var minX = occupied.Min(v => v.X);
            var maxX = occupied.Max(v => v.X);
            var minY = occupied.Min(v => v.Y);
            var maxY = occupied.Max(v => v.Y);

            var xSize = maxX - minX + 1;
            var ySize = maxY - minY + 1;

            var startingPoint = new IntPoint(minX, minY);

            foreach (var point in occupied)
            {
                result.Add(point + position - startingPoint);
            }

            return result.AsReadOnly();
        }

        public IntRect GetRect()
        {
            var occupied = GetOccupiedSpace(new IntPoint(0, 0));
            var minX = occupied.Min(v => v.X);
            var minY = occupied.Min(v => v.Y);
            var maxX = occupied.Max(v => v.X);
            var maxY = occupied.Max(v => v.Y);

            return new IntRect(minX, minY, maxX - minX + 1, maxY - minY + 1);
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(LevelViewPath))
                throw new Exception($"{nameof(LevelViewPath)} is empty");

            if (Placement == null || Placement.Length == 0)
                throw new Exception($"{nameof(Placement)} is empty");

            if (string.IsNullOrEmpty(HandImagePath))
                throw new Exception($"{nameof(HandImagePath)} is empty");

            if (AdjacencyPoints.Count == 0)
                throw new Exception($"{nameof(AdjacencyPoints)} is empty");
        }
    }

    [Serializable]
    public struct Requirements
    {
        public bool DownEdge;
    }
}

