using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units.QueueAnimations
{
    public class RemoveUnitsFromQueue : BaseSequenceStep
    {
        private readonly UnitsCustomerQueueService _queue;
        private readonly UnitsCrowdService _crowd;
        private readonly Action<Unit> _removeFromQueue;

        public RemoveUnitsFromQueue(UnitsCustomerQueueService queue, UnitsCrowdService crowd, Action<Unit> removeFromQueue)
        {
            _queue = queue;
            _crowd = crowd;
            _removeFromQueue = removeFromQueue;
        }

        public override void Play()
        {
            while (_queue.GetUnits().Count > _queue.GetQueueSize())
            {
                var unit = _queue.GetUnits().Last();
                _removeFromQueue(unit);
                _crowd.SendToCrowd(unit, UnitsCrowdService.CrowdDirection.Right);
            }
            FireOnFinished();
        }
    }
}
