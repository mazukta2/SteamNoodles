using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public class BuildingPoints
    {
        public event Action OnPointsChanged = delegate { };
        public event Action OnLevelUp = delegate { };
        public event Action OnMaxLevelUp = delegate { };
        public event Action OnLevelDown = delegate { };
        public int Value { get => _points; set => SetPoints(value); }

        public int CurrentLevel { get; private set; }
        public int MaxCurrentLevel { get; private set; }
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

        public BuildingPoints GetChangedValue(int additionalPoints)
        {
            var newBuildingPoints = new BuildingPoints(_power, _offset);
            newBuildingPoints.Value = _points + additionalPoints;
            return newBuildingPoints;
        }

        private float GetPointsForLevel(int level)
        {
            if (level < 0)
                return 0;

            return (float)(Math.Pow(level, _power) + _offset * level);
        }

        private void SetPoints(int newPointsvalue)
        {
            _points = newPointsvalue;

            while (newPointsvalue >= PointsForNextLevel)
            {
                CurrentLevel++;
                OnLevelUp();

                if (CurrentLevel > MaxCurrentLevel)
                {
                    MaxCurrentLevel = CurrentLevel;
                    OnMaxLevelUp();
                }
            }

            while (newPointsvalue < PointsForCurrentLevel)
            {
                CurrentLevel--;
                OnLevelDown();
            }

            OnPointsChanged();
        }
    }
}
