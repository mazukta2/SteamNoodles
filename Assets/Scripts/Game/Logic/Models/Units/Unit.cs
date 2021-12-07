using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;

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

        public Unit(FloatPoint position, FloatPoint target, ICustomerSettings settings)
        {
            Position = position;
            Target = target;
            Settings = settings;
        }

        public bool MoveToTarget(float delta)
        {
            if (Position.IsClose(Target))
                return true;

            var direction = Target - Position;
            Position = Position + direction.GetNormalize() * delta;
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
        }

        public void SetServed()
        {
            IsServed = true;
        }
    }
}
