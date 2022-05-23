using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.Animations
{
    public class RemoveUnitsFromQueue : BaseSequenceStep
    {
        private readonly CustomerQueue _queue;
        private readonly ICustomers _customers;
        private ICrowd _crowd;
        private readonly Action<Unit> _removeFromQueue;

        public RemoveUnitsFromQueue(CustomerQueue queue, ICustomers customers, ICrowd crowd, Action<Unit> removeFromQueue)
        {
            _queue = queue;
            _customers = customers;
            _crowd = crowd;
            _removeFromQueue = removeFromQueue;
        }

        public override void Play()
        {
            while (_queue.Units.Count > _customers.GetQueueSize())
            {
                var unit = _queue.Units.Last();
                _removeFromQueue(unit);
                _crowd.SendToCrowd(unit, LevelCrowd.CrowdDirection.Right);
            }
            FireOnFinished();
        }
    }
}
