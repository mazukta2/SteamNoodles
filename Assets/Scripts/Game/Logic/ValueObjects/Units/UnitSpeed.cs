using System;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Units
{
    public record UnitSpeed
    {
        public UnitSpeed(float value)
        {
            if (value <= 0) throw new ArgumentException(nameof(value));
            Value = value;
        }

        public float Value { get; }
    }
}
