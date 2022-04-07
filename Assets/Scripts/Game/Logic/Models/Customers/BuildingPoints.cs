using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public class BuildingPoints
    {
        public event Action OnPointsChanged = delegate { };
        public int Value { get => _points; set => SetPoints(value); }

        public int CurrentLevel { get; private set; }
        public int PointsForNextLevel => (int)GetPointsForLevel(CurrentLevel + 1);
        public int PointsForCurrentLevel => (int)GetPointsForLevel(CurrentLevel);
        public float Progress => (float)(Value - PointsForCurrentLevel) / (PointsForNextLevel - PointsForCurrentLevel);

        private int _points;

        public float GetAdditionalProgress(int points)
        {
            return (float)(Value - PointsForCurrentLevel + points) / (PointsForNextLevel - PointsForCurrentLevel);
        }

        private float GetPointsForLevel(int level)
        {
            return (float)(Math.Pow(level, 2) + 2 * level);
        }

        private void SetPoints(int value)
        {
            _points = value;

            while (value >= PointsForNextLevel)
                CurrentLevel++;

            OnPointsChanged();
        }

    }
}
