using System;
using System.Collections.Generic;
using System.Text;
using Game.Assets.Scripts.Game.Logic.Common.Math;

namespace Game.Assets.Scripts.Game.Logic.Common.Calculations
{
    public class PercentCalculator
    {
        private List<PercentModificator> _modificators = new List<PercentModificator>();

        public void Add(PercentModificator modificator, int count = 1)
        {
            if (count <= 0)
                throw new Exception("You cant add zero or negative amount of modificators");

            for (int i = 0; i < count; i++)
                _modificators.Add(modificator);
        }

        public float GetFor(float value, float minValue = float.MinValue, float maxValue = float.MaxValue)
        {
            var modification = (1 + CalculateModificator());
            return GameMath.Clamp(value * modification, minValue, maxValue);
        }

        private float CalculateModificator()
        {
            var percent = 0f;
            foreach (var item in _modificators)
            {
                if (item.Type == PercentModificator.ActionType.Add)
                {
                    percent += item.Value;
                }
                if (item.Type == PercentModificator.ActionType.Remove)
                {
                    percent -= item.Value;
                }
            }
            return percent / 100f;
        }
    }
}
