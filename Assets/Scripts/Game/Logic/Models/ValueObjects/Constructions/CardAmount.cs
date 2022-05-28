using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions
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
