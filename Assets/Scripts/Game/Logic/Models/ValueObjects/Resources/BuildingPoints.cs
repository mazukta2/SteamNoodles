using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources
{
    public record BuildingPoints
    {
        public BuildingPoints(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}
