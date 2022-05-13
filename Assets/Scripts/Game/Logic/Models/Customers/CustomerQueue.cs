using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class CustomerQueue : Disposable
    {
        private readonly ICustomers _customers;
        private IUnits _unitsController;
        private ICrowd _crowd;

        private List<Unit> _queue = new List<Unit>();

        public CustomerQueue(ICustomers customers, IUnits unitsController, ICrowd crowd)
        {
            _customers = customers ?? throw new ArgumentNullException(nameof(customers));
            _unitsController = unitsController ?? throw new ArgumentNullException(nameof(unitsController));
            _crowd = crowd ?? throw new ArgumentNullException(nameof(crowd));
        }

        protected override void DisposeInner()
        {
        }

        public void ServeCustomer()
        {
            var size = _customers.GetQueueSize();
            if (size < 0)
                throw new ArgumentException(nameof(size));

            var first = _queue.FirstOrDefault();
            if (first != null)
            {
                _queue.Remove(first);
                _crowd.SendToCrowd(first, LevelCrowd.CrowdDirection.Left);
            }

            // add new units
            while (size > _queue.Count)
            {
                var pos = GetPositionFor(_queue.Count + 1);
                _queue.Add(_unitsController.SpawnUnit(pos));
            }

            // remove units
            while (size < _queue.Count)
            {
                var last = _queue.Last();
                _queue.Remove(last);
                _crowd.SendToCrowd(last, LevelCrowd.CrowdDirection.Right);
            }

            for (int i = 0; i < _queue.Count; i++)
                _queue[i].SetTarget(GetPositionFor(i));
        }

        private FloatPoint GetPositionFor(int index)
        {
            return _customers.GetQueueFirstPosition() + new FloatPoint(_unitsController.GetUnitSize(), 0) * index;
        }
    }
}
