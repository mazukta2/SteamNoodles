using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public class BuildingPoints
    {
        public event Action OnPointsChanged = delegate { };
        public event Action OnLevelUp = delegate { };
        public int Value { get => _points; set => SetPoints(value); }

        public int CurrentLevel { get; private set; }
        public int PointsForNextLevel => (int)GetPointsForLevel(CurrentLevel + 1);
        public int PointsForCurrentLevel => (int)GetPointsForLevel(CurrentLevel);
        public float Progress => (float)(Value - PointsForCurrentLevel) / (PointsForNextLevel - PointsForCurrentLevel);

        private int _points;
        private float _power;
        private float _offset;

        public BuildingPoints(float power, float offset)
        {
            _power = power;
            _offset = offset;
        }

        public float GetAdditionalProgress(int points)
        {
            return (float)(Value - PointsForCurrentLevel + points) / (PointsForNextLevel - PointsForCurrentLevel);
        }

        private float GetPointsForLevel(int level)
        {
            return (float)(Math.Pow(level, _power) + _offset * level);
        }

        private void SetPoints(int value)
        {
            _points = value;

            while (value >= PointsForNextLevel)
            {
                CurrentLevel++;
                OnLevelUp();
            }

            OnPointsChanged();
        }

    }
}
