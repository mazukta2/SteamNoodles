using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Constructions
{
    public class ConstructionDefinition
    {
        public string Name { get; set; }
        public string LevelViewPath { get; set; }
        public int[,] Placement { get; set; }
        public Requirements Requirements { get; set;  }
        public string HandImagePath { get; set; }
        public int Points { get;  set; }
        [JsonConverter(typeof(DefinitionsDictionaryConventer<ConstructionDefinition, int>))]
        public IReadOnlyDictionary<ConstructionDefinition, int> AdjacencyPoints { get; set; } = new Dictionary<ConstructionDefinition, int>();

        public IReadOnlyCollection<IntPoint> GetOccupiedSpace(IntPoint position, FieldRotation rotation)
        {
            var result = new List<IntPoint>();

            var rotatedPlacement = Rotate(Placement, rotation);

            var occupied = new List<IntPoint>();
            for (int x = 0; x < rotatedPlacement.GetLength(0); x++)
            {
                for (int y = 0; y < rotatedPlacement.GetLength(1); y++)
                {
                    if (rotatedPlacement[x, y] != 0)
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

        private int[,] Rotate(int[,] placement, FieldRotation rotation)
        {
            if (rotation == FieldRotation.Top)
                return placement;

            var result = new int[placement.GetLength(0), placement.GetLength(1)];
            if (rotation == FieldRotation.Right || rotation == FieldRotation.Left)
                result = new int[placement.GetLength(1), placement.GetLength(0)];

            for (int x = 0; x < result.GetLength(0); x++)
            {
                for (int y = 0; y < result.GetLength(1); y++) 
                {
                    var maxX = result.GetLength(0) - 1;
                    var maxY = result.GetLength(1) - 1;

                    if (rotation == FieldRotation.Left)
                        result[x, y] = placement[y, maxX - x];
                    if (rotation == FieldRotation.Right)
                        result[x, y] = placement[maxY - y, x];
                    if (rotation == FieldRotation.Bottom)
                        result[x, y] = placement[maxX - x, maxY - y];
                }
            }
            return result;
        }

        public IntRect GetRect(FieldRotation rotation)
        {
            var occupied = GetOccupiedSpace(new IntPoint(0, 0), rotation);
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

