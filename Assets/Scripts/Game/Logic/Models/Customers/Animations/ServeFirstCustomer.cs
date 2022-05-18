using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.Animations
{
    public class ServeFirstCustomer : BaseQueueStep
    {
        private CustomerQueue _queue;
        private readonly ICrowd _crowd;
        private readonly Action<Unit> _removeFromQueue;
        private readonly Action<Unit> _serve;

        public ServeFirstCustomer(CustomerQueue queue, ICrowd crowd, Action<Unit> removeFromQueue, Action<Unit> serve)
        {
            _queue = queue;
            _crowd = crowd;
            _removeFromQueue = removeFromQueue;
            _serve = serve;
        }

        public override void Play()
        {
            var first = _queue.Units.FirstOrDefault();
            if (first != null)
            {
                _removeFromQueue(first);
                _serve(first);
                _crowd.SendToCrowd(first, LevelCrowd.CrowdDirection.Left);
            }

            FireOnFinished();
        }
    }
}
