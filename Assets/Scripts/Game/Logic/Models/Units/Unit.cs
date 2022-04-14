using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class Unit : Disposable
    {
        public event Action OnPositionChanged = delegate { };
        public event Action OnReachedPosition = delegate { };
        public event Action OnTargetChanged = delegate { };
        public FloatPoint Target { get; private set; }
        public FloatPoint Position { get; private set; }
        public CustomerDefinition Definition { get; private set; }

        private UnitsSettingsDefinition _unitsSettings;
        private float _speedOffset;

        public Unit(FloatPoint position, FloatPoint target, CustomerDefinition definition, UnitsSettingsDefinition unitsSettings, SessionRandom random)
        {
            Position = position;
            Target = target;
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _speedOffset = random.GetRandom(-1, 1) * _unitsSettings.SpeedOffset;
        }

        public bool MoveToTarget(float delta)
        {
            if (Position.IsClose(Target))
                return true;

            var direction = Target - Position;
            var movement = delta * GetSpeed();
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

        public void SetTarget(FloatPoint target)
        {
            Target = target;
            OnTargetChanged();

            var distance = Position.GetDistanceTo(Target);
            if (GetSpeed() * 0.01f > distance)
                TeleportToTarget();
        }

        public bool IsMoving()
        {
            return !Position.IsClose(Target);
        }

        public float GetSpeed()
        {
            return _unitsSettings.Speed + _speedOffset;
        }
    }
}
