using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Units
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
