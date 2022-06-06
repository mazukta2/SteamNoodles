using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units.QueueAnimations
{
    public class AddUnitsToQueue : BaseSequenceStep
    {
        private readonly UnitsCustomerQueueService _queue;
        private readonly UnitsService _unitsController;
        private readonly Action<Unit> _addToQueue;
        private readonly float _delay;
        private readonly TimeUpdater _updater;

        public AddUnitsToQueue(UnitsCustomerQueueService queue, UnitsService unitsController, Action<Unit> addToQueue, IGameTime time, float delay)
        {
            _queue = queue;
            _unitsController = unitsController;
            _addToQueue = addToQueue;
            _delay = delay;
            _updater = new TimeUpdater(time, delay);
        }

        protected override void DisposeInner()
        {
            _updater.Dispose();
            _updater.OnUpdate -= Spawn;
        }

        public override void Play()
        {
            if (IsFinished())
            {
                FireOnFinished();
                return;
            }

            if (_delay == 0)
            {
                while (!IsDisposed)
                    Spawn();
            }
            else
            {
                _updater.OnUpdate += Spawn;
                _updater.Start();
            }

        }

        private bool IsFinished()
        {
            return _queue.GetUnits().Count >= _queue.GetQueueSize().Value;
        }

        private void Spawn()
        {
            if (IsFinished())
            {
                FireOnFinished();
                return;
            }

            var pos = _queue.GetPositionFor(_queue.GetUnits().Count + 1);
            var unit = _unitsController.SpawnUnit(pos);
            _unitsController.LookAt(unit, _queue.GetQueuePosition() + new GameVector3(-1, 0, 0), true);
            _unitsController.Smoke(unit);
            _addToQueue(unit);
        }
    }
}
