using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Units;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.QueueAnimations
{
    public class MoveUnitsToPositionsInQueue : BaseSequenceStep
    {
        private CustomerQueue _queue;
        private readonly ICustomers _customers;
        private Unit[] _list;

        public MoveUnitsToPositionsInQueue(CustomerQueue queue, ICustomers customers)
        {
            _queue = queue;
            _customers = customers;
        }

        protected override void DisposeInner()
        {
            if (_list != null)
            {
                foreach (var unit in _list)
                {
                    unit.OnReachedPosition -= Unit_OnReachedPosition;
                }
            }
        }

        public override void Play()
        {
            _list = _queue.Units.ToArray();
            for (int i = 0; i < _list.Count(); i++)
            {
                _list[i].OnReachedPosition += Unit_OnReachedPosition;
                _list[i].SetTarget(_queue.GetPositionFor(i));
            }
            if (_list.Length == 0)
                FireOnFinished();
        }

        private void Unit_OnReachedPosition()
        {
            if (!_list[0].IsMoving())
            {
                _list[0].LookAt(_customers.GetQueueFirstPosition() + _customers.GetQueueFirstPositionOffset() + new GameVector3(0, 0, 1));
            }

            if (_list.All(x => !x.IsMoving()))
                FireOnFinished();
        }

    }
}
