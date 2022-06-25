using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Services.Session;

namespace Game.Assets.Scripts.Game.Logic.Services.Units.QueueAnimations
{
    public class FreeAllFromQueue : BaseSequenceStep
    {
        private readonly UnitsCustomerQueueService _queue;
        private readonly UnitsCrowdService _crowd;
        private readonly IGameRandom _random;
        private readonly Action<Unit> _removeFromQueue;

        public FreeAllFromQueue(UnitsCustomerQueueService queue, UnitsCrowdService crowd, IGameRandom random, Action<Unit> removeFromQueue)
        {
            _queue = queue;
            _crowd = crowd;
            _random = random;
            _removeFromQueue = removeFromQueue;
        }

        public override void Play()
        {
            foreach (var unit in _queue.GetUnits().ToList())
            {
                _removeFromQueue(unit);
                _crowd.SendToCrowd(unit, _random.GetRandom() ? UnitsCrowdService.CrowdDirection.Right : UnitsCrowdService.CrowdDirection.Left);
            }
            FireOnFinished();
        }
    }
}
