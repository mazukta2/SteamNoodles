using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Units;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.QueueAnimations
{
    public class FreeAllFromQueue : BaseSequenceStep
    {
        private readonly CustomerQueue _queue;
        private readonly ICrowd _crowd;
        private readonly IGameRandom _random;
        private readonly Action<Unit> _removeFromQueue;

        public FreeAllFromQueue(CustomerQueue queue, ICrowd crowd, IGameRandom random, Action<Unit> removeFromQueue)
        {
            _queue = queue;
            _crowd = crowd;
            _random = random;
            _removeFromQueue = removeFromQueue;
        }

        public override void Play()
        {
            foreach (var unit in _queue.Units.ToList())
            {
                _removeFromQueue(unit);
                _crowd.SendToCrowd(unit, _random.GetRandom() ? LevelCrowd.CrowdDirection.Right : LevelCrowd.CrowdDirection.Left);
            }
            FireOnFinished();
        }
    }
}
