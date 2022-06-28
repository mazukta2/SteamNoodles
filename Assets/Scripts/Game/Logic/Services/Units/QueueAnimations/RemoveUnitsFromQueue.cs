using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Entities.Units;

namespace Game.Assets.Scripts.Game.Logic.Services.Units.QueueAnimations
{
    public class RemoveUnitsFromQueue : BaseSequenceStep
    {
        private readonly UnitsCustomerQueueService _queue;
        private readonly UnitsCrowdService _crowd;
        private readonly Action<UnitEntity> _removeFromQueue;

        public RemoveUnitsFromQueue(UnitsCustomerQueueService queue, UnitsCrowdService crowd, Action<UnitEntity> removeFromQueue)
        {
            _queue = queue;
            _crowd = crowd;
            _removeFromQueue = removeFromQueue;
        }

        public override void Play()
        {
            while (_queue.GetUnits().Count > _queue.GetQueueSize().Value)
            {
                var unit = _queue.GetUnits().Last();
                _removeFromQueue(unit);
                _crowd.SendToCrowd(unit, UnitsCrowdService.CrowdDirection.Right);
            }
            FireOnFinished();
        }
    }
}
