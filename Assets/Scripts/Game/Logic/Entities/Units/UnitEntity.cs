using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Events.Units;
using Game.Assets.Scripts.Game.Logic.Services.Session;

namespace Game.Assets.Scripts.Game.Logic.Entities.Units
{
    public record UnitEntity : Entity
    {
        public GameVector3 Target { get; private set; }
        public GameVector3 Position { get; private set; }
        public float CurrentSpeed { get; private set; }

        public int VisualSeed { get; private set; }
        public BehaviourState State { get; private set; }
        public UnitType UnitType { get; private set; }

        private float _speedOffset;

        public UnitEntity(GameVector3 position, GameVector3 target, UnitType type, IGameRandom random)
        {
            UnitType = type ?? throw new ArgumentNullException(nameof(type));
            Position = position;
            Target = target;
            _speedOffset = random.GetRandom(-1, 1) * type.SpeedOffset;
            VisualSeed = random.GetRandom(0, 10000);
            CurrentSpeed = GetMinSpeed();
        }

        public bool IsOnPosition => Position.IsClose(Target);

        public string GetHair() 
        {
            if (UnitType.Hairs == null || UnitType.Hairs.Length == 0)
                return null;

            var random = new Random(VisualSeed);
            return UnitType.Hairs.GetRandom(random);
        }

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

        public void LookAt(GameVector3 target, bool skip = false)
        {
            if (IsMoving())
                throw new Exception("Can't look while moving");

            if ((target - Position).IsZero())
                throw new Exception("Wrong target");

            FireEvent(new UnitLookAtEvent(target, skip));
        }

        public int GetCoins()
        {
            return UnitType.BaseCoins;
        }

        public bool IsMoving()
        {
            return !IsOnPosition;
        }

        public float GetMaxSpeed()
        {
            return UnitType.Speed.Value + _speedOffset;
        }

        private float GetMinSpeed()
        {
            return UnitType.MinSpeed.Value;
        }

        public enum BehaviourState
        {
            Free,
            InCrowd,
            InQueue
        }

    }
}
