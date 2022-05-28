using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions
{
    public record FieldPosition
    {
        public IntPoint Value { get; }

        public FieldPosition(IntPoint value) => (Value) = (value);

        public FieldPosition(int x, int y)
        {
            Value = new IntPoint(x, y);
        }

        public static FieldPosition operator +(FieldPosition current, FieldPosition other) => new FieldPosition(current.Value + other.Value);
    }
}
