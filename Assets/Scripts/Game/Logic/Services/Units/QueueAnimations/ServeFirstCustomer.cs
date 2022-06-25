using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Entities.Units;

namespace Game.Assets.Scripts.Game.Logic.Services.Units.QueueAnimations
{
    public class ServeFirstCustomer : BaseSequenceStep
    {
        private UnitsCustomerQueueService _queue;
        private readonly UnitsCrowdService _crowd;
        private readonly Action<Unit> _serve;

        public ServeFirstCustomer(UnitsCustomerQueueService queue, UnitsCrowdService crowd, Action<Unit> serve)
        {
            _queue = queue;
            _crowd = crowd;
            _serve = serve;
        }

        public override void Play()
        {
            var first = _queue.GetUnits().FirstOrDefault();
            if (first != null)
            {
                _serve(first);
                _crowd.SendToCrowd(first, UnitsCrowdService.CrowdDirection.Left);
            }

            FireOnFinished();
        }
    }
}
