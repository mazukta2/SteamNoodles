using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class Unit : Disposable
    {
        public event Action OnPositionChanged = delegate { };
        public event Action OnReachedPosition = delegate { };
        public event Action OnTargetChanged = delegate { };
        public event Action<GameVector3, bool> OnLookAt = delegate { };
        public GameVector3 Target { get; private set; }
        public GameVector3 Position { get; private set; }
        public CustomerDefinition Definition { get; private set; }

        public int VisualSeed { get; private set; }

        private UnitsSettingsDefinition _unitsSettings;
        private IGameTime _time;
        private float _speedOffset;
        private float _currentSpeed;

        public Unit(GameVector3 position, GameVector3 target, CustomerDefinition definition, 
            UnitsSettingsDefinition unitsSettings, SessionRandom random, IGameTime time)
        {
            Position = position;
            Target = target;
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _time = time;
            _speedOffset = random.GetRandom(-1, 1) * _unitsSettings.SpeedOffset;
            VisualSeed = random.GetRandom(0, 10000);
            _currentSpeed = GetMinSpeed();

            _time.OnTimeChanged += Time_OnTimeChanged;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= Time_OnTimeChanged;
        }

        private void Time_OnTimeChanged(float oldTime, float newTime)
        {
            UpdateSpeed(newTime - oldTime);
            MoveToTarget(newTime - oldTime);
        }

        private void UpdateSpeed(float delta)
        {
            var speedUp = Position.GetDistanceTo(Target) > _unitsSettings.SpeedUpDistance;

            if (speedUp)
            {
                _currentSpeed += delta * _unitsSettings.SpeedUp;
                _currentSpeed = Math.Min(_currentSpeed, GetMaxSpeed());
            }
            else
            {
                _currentSpeed -= delta * _unitsSettings.SpeedUp;
                _currentSpeed = Math.Max(_currentSpeed, GetMinSpeed());
            }
        }

        private bool MoveToTarget(float delta)
        {
            if (Position.IsClose(Target))
                return true;

            var direction = Target - Position;
            var movement = delta * GetCurrentSpeed();
            var distance = Position.GetDistanceTo(Target);
            if (distance < movement)
                movement = distance;

            var normalizeDirection = direction.GetNormalize();
            Position = Position + normalizeDirection * movement;
            if (Position.Y != 0)
                throw new Exception();

            if (Position.IsClose(Target))
            {
                Position = Target;
                OnPositionChanged();
                OnReachedPosition();
            }
            else
                OnPositionChanged();

            return false;
        }

        public void LookAt(GameVector3 target, bool skip = false)
        {
            if (IsMoving())
                throw new Exception("Can't look while moving");

            if ((target - Position).IsZero())
                throw new Exception("Wrong target");

            OnLookAt(target, skip);
        }

        public void SetPosition(GameVector3 target)
        {
            SetTarget(target);
            TeleportToTarget();
        }

        public void TeleportToTarget()
        {
            if (Position.IsClose(Target))
                return;

            Position = Target;
            OnPositionChanged();
            OnReachedPosition();
        }

        public void SetTarget(GameVector3 target)
        {
            if (target.Y != 0)
                throw new Exception();
            Target = target;
            OnTargetChanged();

            var distance = Position.GetDistanceTo(Target);
            if (GetCurrentSpeed() * 0.01f > distance)
                TeleportToTarget();
        }

        public bool IsMoving()
        {
            return !Position.IsClose(Target);
        }

        public float GetMaxSpeed()
        {
            return _unitsSettings.Speed + _speedOffset;
        }

        public float GetCurrentSpeed()
        {
            return _currentSpeed;
        }

        private float GetMinSpeed()
        {
            return _unitsSettings.MinSpeed;
        }

    }
}
