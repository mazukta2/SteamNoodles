using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units.QueueAnimations
{
    public class ServeFirstCustomer : BaseSequenceStep
    {
        private UnitsCustomerQueueService _queue;
        private readonly UnitsCrowdService _crowd;
        private readonly Action<Unit> _removeFromQueue;
        private readonly Action<Unit> _serve;

        public ServeFirstCustomer(UnitsCustomerQueueService queue, UnitsCrowdService crowd, Action<Unit> removeFromQueue, Action<Unit> serve)
        {
            _queue = queue;
            _crowd = crowd;
            _removeFromQueue = removeFromQueue;
            _serve = serve;
        }

        public override void Play()
        {
            var first = _queue.GetUnits().FirstOrDefault();
            if (first != null)
            {
                _removeFromQueue(first);
                _serve(first);
                _crowd.SendToCrowd(first, UnitsCrowdService.CrowdDirection.Left);
            }

            FireOnFinished();
        }
    }
}
