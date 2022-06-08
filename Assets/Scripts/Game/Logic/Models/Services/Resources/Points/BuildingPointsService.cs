using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points.BuildingPointsAnimations;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points
{
    public class BuildingPointsService : Disposable, IService
    {
        public event Action OnPointsChanged = delegate { };
        public event Action OnCurrentLevelUp = delegate { };
        public event Action OnCurrentLevelDown = delegate { };
        public event Action<int> OnTargetLevelChanged = delegate { };
        public event Action OnMaxTargetLevelUp = delegate { };
        public event Action<AddPointsAnimation> OnAnimationStarted = delegate { };

        private readonly IGameTime _time;
        private readonly float _pieceSpawningTime;
        private readonly float _pieceMovingTime;
        private List<AddPointsAnimation> _animations = new List<AddPointsAnimation>();
        private BuildingPointsCalculator _current;
        private BuildingPointsCalculator _target;
        private int _maxTargetLevel;


        public BuildingPointsService() : this(0, 0, new GameTime(), 2, 2)
        {
            
        }

        public BuildingPointsService(float pieceSpawningTime, float pieceMovingTime, IGameTime time, float power, float offset)
        {
            _pieceSpawningTime = pieceSpawningTime;
            _pieceMovingTime = pieceMovingTime;
            _time = time;
            _current = new BuildingPointsCalculator(power, offset);
            _target = _current.Copy();
        }

        public BuildingPointsService(StageLevel level, IGameTime time)
            : this(level.PieceSpawningTime, level.PieceMovingTime, time, level.LevelUpPower, level.LevelUpOffset)
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


        public int GetValue()
        {
            return _current.Value;
        }

        public BuildingLevel GetCurrentLevel()
        {
            return new BuildingLevel(_current.CurrentLevel);
        }

        public BuildingLevel GetTargetLevel()
        {
            return new BuildingLevel(_target.CurrentLevel);
        }

        public int GetMaxTargetLevel()
        {
            return _maxTargetLevel;
        }

        public int GetPointsForNextLevel()
        {
            return _current.PointsForNextLevel;
        }

        public int GetPointsForCurrentLevel()
        {
            return _current.PointsForCurrentLevel;
        }

        public float GetProgress()
        {
            return _current.Progress;
        }
        public void Change(BuildingPoints value)
        {
            Change(value, GameVector3.Zero);
        }

        public void Change(BuildingPoints value, GameVector3 postion)
        {
            var targetLevel = _target.CurrentLevel;
            _target.Value += value.Value;

            if (_target.CurrentLevel != targetLevel)
                OnTargetLevelChanged(_target.CurrentLevel - targetLevel);

            if (value.Value > 0)
            {
                var animation = new AddPointsAnimation(value.Value, _pieceSpawningTime, _pieceMovingTime, _time, postion);
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

                if (_target.CurrentLevel > GetMaxTargetLevel())
                {
                    SetMaxTargetLevel(_target.CurrentLevel);
                    OnMaxTargetLevelUp();
                }
            }
            else if (value.Value < 0)
                ChangePointsInner(value.Value);
        }

        private void ChangePointsInner(int changes)
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
            ChangePointsInner(1);
        }

        private void SetMaxTargetLevel(int value)
        {
            _maxTargetLevel = value;
        }
    }
}
