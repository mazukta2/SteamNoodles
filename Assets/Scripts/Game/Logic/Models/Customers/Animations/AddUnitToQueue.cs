using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.Animations
{
    public class AddUnitToQueue : BaseQueueStep
    {
        private readonly CustomerQueue _queue;
        private readonly ICustomers _customers;
        private readonly IUnits _unitsController;
        private readonly Action<Unit> _addToQueue;

        public AddUnitToQueue(CustomerQueue queue, ICustomers customers, IUnits unitsController, Action<Unit> addToQueue)
        {
            _queue = queue;
            _customers = customers;
            _unitsController = unitsController;
            _addToQueue = addToQueue;
        }

        public override void Play()
        {
            var pos = _queue.GetPositionFor(_queue.Units.Count + 1);
            var unit = _unitsController.SpawnUnit(pos);
            unit.LookAt(_customers.GetQueueFirstPosition() + new GameVector3(-1, 0, 0), true);
            _addToQueue(unit);
            FireOnFinished();
        }
    }
}
