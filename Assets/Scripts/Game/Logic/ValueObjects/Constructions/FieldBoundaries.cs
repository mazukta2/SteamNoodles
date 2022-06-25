using System;
using Game.Assets.Scripts.Game.Logic.Common.Math;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions
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

        public bool IsInside(FieldPosition position)
        {
            return Value.IsInside(position.Value);
        }
    }
}
