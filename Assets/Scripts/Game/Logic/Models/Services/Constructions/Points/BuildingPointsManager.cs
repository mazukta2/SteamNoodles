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
        public event Action OnCurrentLevelUp = delegate { };
        public event Action OnCurrentLevelDown = delegate { };
        public event Action<int> OnTargetLevelChanged = delegate { };
        public event Action OnMaxTargetLevelUp = delegate { };
        public event Action<AddPointsAnimation> OnAnimationStarted = delegate { };
        public int Value { get => _current.Value; }
        public int CurrentLevel => _current.CurrentLevel;
        public int TargetLevel => _target.CurrentLevel;
        public int MaxTargetLevel { get; private set; }
        public int PointsForNextLevel => _current.PointsForNextLevel;
        public int PointsForCurrentLevel => _current.PointsForCurrentLevel;
        public float Progress => _current.Progress;

        private readonly IGameTime _time;
        private readonly float _pieceSpawningTime;
        private readonly float _pieceMovingTime;
        private List<AddPointsAnimation> _animations = new List<AddPointsAnimation>();
        private BuildingPointsCalculator _current;
        private BuildingPointsCalculator _target;

        public BuildingPointsManager(float pieceSpawningTime, float pieceMovingTime, IGameTime time, float power, float offset)
        {
            _pieceSpawningTime = pieceSpawningTime;
            _pieceMovingTime = pieceMovingTime;
            _time = time;
            _current = new BuildingPointsCalculator(power, offset);
            _target = _current.Copy();
        }

        public BuildingPointsManager(ConstructionsSettingsDefinition constructionSettingsDefinition, IGameTime time, float power, float offset)
            : this(constructionSettingsDefinition.PieceSpawningTime, constructionSettingsDefinition.PieceMovingTime, time, power, offset)
        {
        }

        protected override void DisposeInner()
        {
            foreach (var anim in _animations.ToArray())
                anim.Dispose();
        }

        public BuildingPointsCalculator GetChangedValue(int additionalPoints)
        {
            var newBuildingPoints = _current.Copy();
            newBuildingPoints.Value = _current.Value + additionalPoints;
            return newBuildingPoints;
        }

        public void Change(int value) 
        {
            Change(value, GameVector3.Zero);
        }

        public void Change(int value, GameVector3 postion)
        {
            var targetLevel = _target.CurrentLevel;
            _target.Value += value;

            if (_target.CurrentLevel != targetLevel)
                OnTargetLevelChanged(_target.CurrentLevel - targetLevel);

            if (value > 0)
            {
                var animation = new AddPointsAnimation(value, _pieceSpawningTime, _pieceMovingTime, _time, postion);
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

                if (_target.CurrentLevel > MaxTargetLevel)
                {
                    MaxTargetLevel = _target.CurrentLevel;
                    OnMaxTargetLevelUp();
                }
            }
            else if (value < 0)
                ChangePoints(value);
        }

        private void ChangePoints(int changes)
        {
            var currentLevel = _current.CurrentLevel;
            _current.Value += changes;

            if (currentLevel < _current.CurrentLevel)
                OnCurrentLevelUp();

            if (currentLevel > _current.CurrentLevel)
                OnCurrentLevelDown();

            OnPointsChanged();
        }

        private void Animation_OnPieceReachDestination()
        {
            ChangePoints(1);
        }

    }
}
