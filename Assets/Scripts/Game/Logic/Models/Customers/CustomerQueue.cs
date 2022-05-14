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
                first.OnReachedPosition -= Unit_OnReachedPosition;
                _queue.Remove(first);
                _crowd.SendToCrowd(first, LevelCrowd.CrowdDirection.Left);
            }

            // add new units
            while (size > _queue.Count)
            {
                var pos = GetPositionFor(_queue.Count + 1);
                var unit = _unitsController.SpawnUnit(pos);
                unit.LookAt(_customers.GetQueueFirstPosition(), true);
                unit.OnReachedPosition += Unit_OnReachedPosition;
                _queue.Add(unit);
            }

            // remove units
            while (size < _queue.Count)
            {
                var last = _queue.Last();
                last.OnReachedPosition -= Unit_OnReachedPosition;
                _queue.Remove(last);
                _crowd.SendToCrowd(last, LevelCrowd.CrowdDirection.Right);
            }

            for (int i = 0; i < _queue.Count; i++)
                _queue[i].SetTarget(GetPositionFor(i));
        }

        private void Unit_OnReachedPosition()
        {
            if (_queue.Count > 0)
            {
                if (!_queue[0].IsMoving())
                {
                    _queue[0].LookAt(_customers.GetQueueFirstPosition() + _customers.GetQueueFirstPositionOffset() + new FloatPoint3D(0, 0, 1));
                }
            }
        }

        private FloatPoint3D GetPositionFor(int index)
        {
            var offset = FloatPoint3D.Zero;
            if (index == 0)
                offset = _customers.GetQueueFirstPositionOffset();

            return _customers.GetQueueFirstPosition() + offset + new FloatPoint3D(_unitsController.GetUnitSize(), 0, 0) * index;
        }
    }
}
