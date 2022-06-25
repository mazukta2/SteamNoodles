using System;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions
{
    public record CardAmount
    {
        public CardAmount(int value)
        {
            if (value < 0) throw new ArgumentException(nameof(value));
            Value = value;
        }

        public int Value { get; }
    }
}
