using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Effects.Systems;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class Unit
    {
        public event Action OnPositionChanged = delegate { };
        public event Action OnReachedPosition = delegate { };
        public FloatPoint Target { get; private set; }
        public FloatPoint Position { get; private set; }
        public bool IsServed { get; private set; }
        public ICustomerSettings Settings { get; private set; }

        private UnitServicing _unitServing;

        public Unit(FloatPoint position, FloatPoint target, ICustomerSettings settings, UnitServicing unitServing)
        {
            Position = position;
            Target = target;
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _unitServing = unitServing ?? throw new ArgumentNullException(nameof(unitServing));
        }

        public bool MoveToTarget(float delta)
        {
            if (Position.IsClose(Target))
                return true;

            var direction = Target - Position;
            var movement =  delta * Settings.Speed;
            var distance = Position.GetDistanceTo(Target);
            if (distance < movement)
                movement = distance;

            Position = Position + direction.GetNormalize() * movement;
            OnPositionChanged();

            if (Position.IsClose(Target))
                OnReachedPosition();

            return false;
        }

        public void TeleportToTarget()
        {
            if (Position.IsClose(Target))
                return;

            Position = Target;
            OnReachedPosition();
        }

        public bool CanOrder()
        {
            return !IsServed;
        }

        public void SetTarget(FloatPoint target)
        {
            Target = target;

            var distance = Position.GetDistanceTo(Target);
            if (Settings.Speed * 0.01f > distance)
                TeleportToTarget();
        }

        public void SetServed()
        {
            IsServed = true;
        }

        public bool IsMoving()
        {
            return !Position.IsClose(Target);
        }

        public float GetOrderingTime()
        {
            return Settings.OrderingTime;
        }

        public float GetCookingTime()
        {
            return Settings.CookingTime;
        }

        public float GetEatingTime()
        {
            return _unitServing.GetEatingTime(this);
        }

        public int GetServingMoney()
        {
            return _unitServing.GetServingMoney(this);
        }

        public int GetTips()
        {
            return _unitServing.GetTips(this);
        }
    }
}
