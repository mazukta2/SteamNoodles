using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Services.Units
{
    public class UnitsMovementsService : Disposable, IService
    {
        private IDatabase<Unit> _units;
        private IGameTime _time;

        public UnitsMovementsService(IDatabase<Unit> units, IGameTime time)
        {
            _units = units;
            _time = time;
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
            foreach (var unit in _units.Get())
            {
                var speedUp = unit.Position.GetDistanceTo(unit.Target) > unit.UnitType.SpeedUpDistance;
                var currentSpeed = unit.CurrentSpeed;
                if (speedUp)
                    currentSpeed += delta * unit.UnitType.SpeedUp;
                else
                    currentSpeed -= delta * unit.UnitType.SpeedUp;

                unit.SetTargetSpeed(currentSpeed);
            }
        }

        private void MoveToTarget(float delta)
        {
            foreach (var unit in _units.Get())
            {
                if (unit.IsOnPosition)
                    continue;

                var direction = unit.Target - unit.Position;
                var movement = delta * unit.CurrentSpeed;
                var distance = unit.Position.GetDistanceTo(unit.Target);
                if (distance < movement)
                    movement = distance;

                var normalizeDirection = direction.GetNormalize();
                unit.SetPosition(unit.Position + normalizeDirection * movement);
                if (unit.Position.Y != 0)
                    throw new Exception();
            }
        }
    }
}
