using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units
{
    public class UnitsMovementsService : Disposable
    {
        private IRepository<Unit> _units;
        private IGameTime _time;
        private readonly float _speedUp;
        private readonly float _speedUpDistance;

        public UnitsMovementsService(IRepository<Unit> units, UnitsSettingsDefinition unitsSettings, IGameTime time)
            : this(units, time, unitsSettings.SpeedUp, unitsSettings.SpeedUpDistance)
        {
        }

        public UnitsMovementsService(IRepository<Unit> units, IGameTime time, float speedUp = 1, float speedUpDistance = 0)
        {
            _units = units;
            _time = time;
            _speedUp = speedUp;
            _speedUpDistance = speedUpDistance;
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
                var speedUp = unit.Position.GetDistanceTo(unit.Target) > _speedUpDistance;
                var currentSpeed = unit.CurrentSpeed;
                if (speedUp)
                    currentSpeed += delta * _speedUp;
                else
                    currentSpeed -= delta * _speedUp;

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

                _units.Save(unit);
            }
        }
    }
}
