using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units
{
    public class UnitsMovementsService : Disposable
    {
        private IRepository<Unit> _units;
        private IGameTime _time;
        private UnitsSettingsDefinition _unitsSettings;

        public UnitsMovementsService(IRepository<Unit> units, UnitsSettingsDefinition unitsSettings, IGameTime time)
        {
            _units = units;
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
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
                var speedUp = unit.Position.GetDistanceTo(unit.Target) > _unitsSettings.SpeedUpDistance;
                var currentSpeed = unit.CurrentSpeed;
                if (speedUp)
                    currentSpeed += delta * _unitsSettings.SpeedUp;
                else
                    currentSpeed -= delta * _unitsSettings.SpeedUp;

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

                var reachedPosition = false;
                if (unit.IsOnPosition)
                {
                    unit.SetPosition(unit.Target);
                    reachedPosition = true;
                }

                _units.Save(unit);

                _units.FireEvent(unit, new UnitPositionChangedEvent());
                if (reachedPosition)
                    _units.FireEvent(unit, new UnitReachedTargetPositionEvent());
            }
        }
    }
}
