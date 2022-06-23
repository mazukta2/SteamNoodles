using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions
{
    public record ContructionPlacement
    {
        public static readonly ContructionPlacement One = new ContructionPlacement(new[,] { { 1 } });

        private int[,] _placement;

        public ContructionPlacement(int[,] placement)
        {
            _placement = placement;
        }

        public int GetHeight(FieldRotation rotation)
        {
            return GetRect(rotation).Height;
        }

        public IntRect GetRect(FieldRotation rotation)
        {
            var occupied = GetOccupiedSpace(new CellPosition(0, 0), rotation);
            var minX = occupied.Min(v => v.X);
            var minY = occupied.Min(v => v.Y);
            var maxX = occupied.Max(v => v.X);
            var maxY = occupied.Max(v => v.Y);

            return new IntRect(minX, minY, maxX - minX + 1, maxY - minY + 1);
        }

        public IReadOnlyCollection<CellPosition> GetOccupiedSpace(CellPosition position, FieldRotation rotation)
        {
            var result = new List<CellPosition>();

            var rotatedPlacement = Rotate(_placement, rotation);

            var occupied = new List<CellPosition>();
            for (int x = 0; x < rotatedPlacement.GetLength(0); x++)
            {
                for (int y = 0; y < rotatedPlacement.GetLength(1); y++)
                {
                    if (rotatedPlacement[x, y] != 0)
                        occupied.Add(new CellPosition(x, y));
                }
            }

            var minX = occupied.Min(v => v.X);
            var maxX = occupied.Max(v => v.X);
            var minY = occupied.Min(v => v.Y);
            var maxY = occupied.Max(v => v.Y);

            var xSize = maxX - minX + 1;
            var ySize = maxY - minY + 1;

            var startingPoint = new CellPosition(minX, minY);

            foreach (var point in occupied)
            {
                result.Add(point + position - startingPoint);
            }

            return result.AsReadOnly();
        }

        private int[,] Rotate(int[,] placement, FieldRotation rotation)
        {
            if (rotation.Value == FieldRotation.Rotation.Top)
                return placement;

            var result = new int[placement.GetLength(0), placement.GetLength(1)];
            if (rotation.Value == FieldRotation.Rotation.Right || rotation.Value == FieldRotation.Rotation.Left)
                result = new int[placement.GetLength(1), placement.GetLength(0)];

            for (int x = 0; x < result.GetLength(0); x++)
            {
                for (int y = 0; y < result.GetLength(1); y++)
                {
                    var maxX = result.GetLength(0) - 1;
                    var maxY = result.GetLength(1) - 1;

                    if (rotation.Value == FieldRotation.Rotation.Left)
                        result[x, y] = placement[y, maxX - x];
                    if (rotation.Value == FieldRotation.Rotation.Right)
                        result[x, y] = placement[maxY - y, x];
                    if (rotation.Value == FieldRotation.Rotation.Bottom)
                        result[x, y] = placement[maxX - x, maxY - y];
                }
            }
            return result;
        }

    }
}
