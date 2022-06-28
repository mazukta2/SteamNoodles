using System;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.QueueAnimations
{
    public class AddUnitsToQueue : BaseSequenceStep
    {
        private readonly CustomerQueue _queue;
        private readonly ICustomers _customers;
        private readonly IUnits _unitsController;
        private readonly Action<Unit> _addToQueue;
        private readonly float _delay;
        private readonly TimeUpdater _updater;

        public AddUnitsToQueue(CustomerQueue queue, ICustomers customers, IUnits unitsController, Action<Unit> addToQueue, IGameTime time, float delay)
        {
            _queue = queue;
            _customers = customers;
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
            return _queue.Units.Count >= _customers.GetQueueSize();
        }

        private void Spawn()
        {
            if (IsFinished())
            {
                FireOnFinished();
                return;
            }

            var pos = _queue.GetPositionFor(_queue.Units.Count + 1);
            var unit = _unitsController.SpawnUnit(pos);
            unit.LookAt(_customers.GetQueueFirstPosition() + new GameVector3(-1, 0, 0), true);
            unit.Smoke();
            _addToQueue(unit);
        }
    }
}
