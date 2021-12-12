using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Calculations
{
    public class PercentModificator
    {
        public ActionType Type { get; }
        public float Value { get; }

        public PercentModificator(ActionType type, float value)
        {
            Type = type;
            Value = value;
        }

        public enum ActionType
        {
            Add,
            Remove
        }
    }
}
