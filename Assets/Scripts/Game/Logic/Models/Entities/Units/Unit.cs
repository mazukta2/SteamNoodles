using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Units
{
    public record Unit : Entity
    {
        public GameVector3 Target { get; private set; }
        public GameVector3 Position { get; private set; }
        public float CurrentSpeed { get; private set; }

        public int VisualSeed { get; private set; }
        public BehaviourState State { get; private set; }

        private float _speedOffset;
        private float _minSpeed;
        private float _speed;

        public Unit(GameVector3 position, GameVector3 target, CustomerDefinition definition,
            UnitsSettingsDefinition unitsSettings, IGameRandom random)
        {
            Position = position;
            Target = target;
            _speedOffset = random.GetRandom(-1, 1) * unitsSettings.SpeedOffset;
            _minSpeed = unitsSettings.MinSpeed;
            _speed = unitsSettings.Speed;
            VisualSeed = random.GetRandom(0, 10000);
            CurrentSpeed = GetMinSpeed();
        }

        public Unit(GameVector3 position, GameVector3 target, UnitType type, IGameRandom random)
        {
            Position = position;
            Target = target;
            _speedOffset = random.GetRandom(-1, 1) * type.SpeedOffset;
            _minSpeed = type.MinSpeed.Value;
            _speed = type.Speed.Value;
            VisualSeed = random.GetRandom(0, 10000);
            CurrentSpeed = GetMinSpeed();
        }

        public Unit(GameVector3 position, GameVector3 target, float minSpeed, float speed, float speedOffset, int visualSeed)
        {
            Position = position;
            Target = target;
            _speedOffset = speedOffset;
            _minSpeed = minSpeed;
            _speed = speed;
            VisualSeed = visualSeed;
            CurrentSpeed = GetMinSpeed();

        }
        public bool IsOnPosition => Position.IsClose(Target);
        public void SetTargetSpeed(float targetSpeed)
        {
            targetSpeed = Math.Min(targetSpeed, GetMaxSpeed());
            targetSpeed = Math.Max(targetSpeed, GetMinSpeed());

            CurrentSpeed = targetSpeed;
        }

        public void SetPosition(GameVector3 newPosition)
        {
            Position = newPosition;
            FireEvent(new UnitPositionChangedEvent());
            if (IsOnPosition)
            {
                Position = Target;
                FireEvent(new UnitReachedTargetPositionEvent());
            }
        }

        public void SetTarget(GameVector3 target)
        {
            if (target.Y != 0)
                throw new Exception();
            Target = target;
            FireEvent(new UnitTargetChangedEvent());

            var distance = Position.GetDistanceTo(Target);
            if (CurrentSpeed * 0.01f > distance)
                TeleportToTarget();
        }

        public void SetBehaviourState(BehaviourState state)
        {
            State = state;
        }

        private void TeleportToTarget()
        {
            if (Position.IsClose(Target))
                return;

            Position = Target;
            FireEvent(new UnitReachedTargetPositionEvent());
        }

        public bool IsMoving()
        {
            return !IsOnPosition;
        }

        public float GetMaxSpeed()
        {
            return _speed + _speedOffset;
        }

        private float GetMinSpeed()
        {
            return _minSpeed;
        }

        public enum BehaviourState
        {
            Free,
            InCrowd,
            InQueue
        }
    }
}
