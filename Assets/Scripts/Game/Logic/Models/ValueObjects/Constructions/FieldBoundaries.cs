using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions
{
    public record FieldBoundaries
    {
        public IntRect Value { get; }

        public FieldBoundaries(IntPoint size)
        {
            if (size.X <= 0 || size.Y <= 0)
                throw new Exception("Wrong size");

            if (size.X % 2 == 0 || size.Y % 2 == 0)
                throw new Exception("Even sized fields are not supported. Sorry :(");

            Value = new IntRect(-size.X / 2, -size.Y / 2, size.X, size.Y);
        }

        public bool IsInside(CellPosition position)
        {
            return Value.IsInside(position.Value);
        }
    }
}
