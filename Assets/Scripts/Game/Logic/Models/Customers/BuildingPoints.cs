using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public class BuildingPoints
    {
        public int Points { get => _points; set => SetPoints(value); }

        public int CurrentLevel { get; private set; }
        public int PointsForNextLevel => (int)GetPointsForLevel(CurrentLevel + 1);
        public float Progress => (float)Points / PointsForNextLevel;

        private int _points;

        private float GetPointsForLevel(int level)
        {
            return (float)(Math.Pow(level, 2) + 2 * level);
        }

        private void SetPoints(int value)
        {
            _points = value;

            while (value >= PointsForNextLevel)
                CurrentLevel++;
        }

    }
}
