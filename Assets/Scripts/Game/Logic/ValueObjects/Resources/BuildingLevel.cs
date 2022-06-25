using System;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Resources
{
    public record BuildingLevel
    {
        public int Value { get => _value; }
        private int _value;

        public BuildingLevel(int value)
        {
            if (value < 0) throw new ArgumentException(nameof(value));

            _value = value;
        }

    }
}
