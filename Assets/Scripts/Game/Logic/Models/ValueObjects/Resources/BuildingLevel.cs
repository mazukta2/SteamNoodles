using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources
{
    public record BuildingLevel
    {
        private int _value;

        public BuildingLevel(int value)
        {
            if (value < 0) throw new ArgumentException(nameof(value));

            _value = value;
        }
    }
}
