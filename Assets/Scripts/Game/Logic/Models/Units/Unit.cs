using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class Unit
    {
        public event Action OnPositionChanged = delegate { };
        public FloatPoint Target { get; private set; }
        public FloatPoint Position { get; private set; }
        public Unit(FloatPoint position, FloatPoint target)
        {
            Position = position;
            Target = target;
        }

        public bool MoveToTarget(float delta)
        {
            if (Position.IsClose(Target))
                return true;

            var direction = Target - Position;
            Position = Position + direction.GetNormalize() * delta;
            OnPositionChanged();
            return false;    
        }

        public bool CanOrder()
        {
            return true;
        }

        public void SetTarget(FloatPoint target)
        {
            Target = target;
        }
    }
}
