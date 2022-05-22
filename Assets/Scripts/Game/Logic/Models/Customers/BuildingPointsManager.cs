using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers.BuildingPointsAnimations;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public class BuildingPointsManager : Disposable
    {
        public event Action OnPointsChanged = delegate { };
        public event Action OnLevelUp = delegate { };
        public event Action OnMaxTargetLevelUp = delegate { };
        public event Action OnLevelDown = delegate { };
        public event Action<AddPointsAnimation> OnAnimationStarted = delegate { };
        public int Value { get => _calculator.Value; }
        public int CurrentLevel => _calculator.CurrentLevel;
        public int MaxTargettLevel { get; private set; }
        public int PointsForNextLevel => _calculator.PointsForNextLevel;
        public int PointsForCurrentLevel => _calculator.PointsForCurrentLevel;
        public float Progress => _calculator.Progress;

        private readonly ConstructionsSettingsDefinition _constructionSettingsDefinition;
        private readonly IGameTime _time;
        private List<AddPointsAnimation> _animations = new List<AddPointsAnimation>();
        private BuildingPointsCalculator _calculator;
        private BuildingPointsCalculator _target;

        public BuildingPointsManager(ConstructionsSettingsDefinition constructionSettingsDefinition, IGameTime time, float power, float offset)
        {
            _constructionSettingsDefinition = constructionSettingsDefinition;
            _time = time;
            _calculator = new BuildingPointsCalculator(power, offset);
            _target = _calculator.Copy();
        }

        protected override void DisposeInner()
        {
            foreach (var anim in _animations)
                anim.Dispose();
        }

        public BuildingPointsCalculator GetChangedValue(int additionalPoints)
        {
            var newBuildingPoints = _calculator.Copy();
            newBuildingPoints.Value = _calculator.Value + additionalPoints;
            return newBuildingPoints;
        }

        public void Change(int value) 
        {
            Change(value, GameVector3.Zero);
        }

        public void Change(int value, GameVector3 postion)
        {
            _target.Value += value;

            if (value > 0)
            {
                var animation = new AddPointsAnimation(value, _constructionSettingsDefinition, _time, postion);
                animation.OnDispose += Animation_OnDispose;
                animation.OnPieceReachDestination += Animation_OnPieceReachDestination;
                _animations.Add(animation);
                OnAnimationStarted(animation);
                animation.Play();
                void Animation_OnDispose()
                {
                    animation.OnPieceReachDestination -= Animation_OnPieceReachDestination;
                    animation.OnDispose -= Animation_OnDispose;
                    _animations.Remove(animation);
                }

                if (_target.CurrentLevel > MaxTargettLevel)
                {
                    MaxTargettLevel = _target.CurrentLevel;
                    OnMaxTargetLevelUp();
                }
            }
            else if (value < 0)
                ChangePoints(value);
        }

        private void ChangePoints(int changes)
        {
            var currentLevel = _calculator.CurrentLevel;
            _calculator.Value += changes;

            if (currentLevel < _calculator.CurrentLevel)
                OnLevelUp();

            if (currentLevel > _calculator.CurrentLevel)
                OnLevelDown();

            OnPointsChanged();
        }

        private void Animation_OnPieceReachDestination()
        {
            ChangePoints(1);
        }

    }
}
