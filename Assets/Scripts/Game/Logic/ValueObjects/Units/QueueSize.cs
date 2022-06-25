using System;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Units
{
    public record QueueSize
    {
        public QueueSize(float value)
        {
            if (value < 0) throw new ArgumentException(nameof(value));
            Value = value;
        }

        public float Value { get; }
    }
}
