using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers.BuildingPointsAnimations;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public class BuildingPointsCalculator
    {
        public int Value { get; set; }
        public int CurrentLevel => GetLevelForPoints(Value);
        public int PointsForNextLevel => GetPointsForLevel(CurrentLevel + 1);
        public int PointsForCurrentLevel => GetPointsForLevel(CurrentLevel);
        public float Progress => (float)(Value - PointsForCurrentLevel) / (PointsForNextLevel - PointsForCurrentLevel);

        private float _power;
        private float _offset;

        public BuildingPointsCalculator(float power, float offset)
        {
            _power = power;
            _offset = offset;
        }

        private int GetPointsForLevel(int level)
        {
            if (level < 0)
                return 0;

            return (int)(Math.Pow(level, _power) + _offset * level);
        }

        private int GetLevelForPoints(int points)
        {
            if (points <= 0)
                return 0;

            var level = 0;
            while (true)
            {
                var min = GetPointsForLevel(level);
                var max = GetPointsForLevel(level + 1);
                if (min <= points && points < max)
                    return level;

                level++;
            }
        }

        public BuildingPointsCalculator Copy()
        {
            return new BuildingPointsCalculator(_power, _offset);
        }
    }
}
