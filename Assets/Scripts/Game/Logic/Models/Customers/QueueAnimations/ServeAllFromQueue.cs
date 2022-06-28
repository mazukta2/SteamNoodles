using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.QueueAnimations
{
    public class ServeAllFromQueue : BaseSequenceStep
    {
        private readonly CustomerQueue _queue;
        private readonly float _delay;
        private readonly ICrowd _crowd;
        private readonly IGameRandom _random;
        private readonly Action<Unit> _removeFromQueue;
        private readonly Action<Unit> _serve;
        private readonly TimeUpdater _updater;

        public ServeAllFromQueue(CustomerQueue queue, ICrowd crowd, IGameRandom random, Action<Unit> removeFromQueue, Action<Unit> serve, IGameTime time, float delay)
        {
            _queue = queue;
            _delay = delay;
            _crowd = crowd;
            _random = random;
            _removeFromQueue = removeFromQueue;
            _serve = serve;
            _updater = new TimeUpdater(time, delay);
        }

        protected override void DisposeInner()
        {
            _updater.Dispose();
            _updater.OnUpdate -= Update;
        }

        public override void Play()
        {
            _updater.OnUpdate += Update;
            _updater.Start();
            Update();
        }

        private void Serve()
        {
            if (_queue.Units.Count == 0)
            {
                FireOnFinished();
                return;
            }

            var first = _queue.Units.First();
            _removeFromQueue(first);
            _serve(first);
            _crowd.SendToCrowd(first, _random.GetRandom() ? LevelCrowd.CrowdDirection.Right : LevelCrowd.CrowdDirection.Left);

            if (_queue.Units.Count == 0)
            {
                FireOnFinished();
                return;
            }
        }

        private void Update()
        {
            if (_delay == 0)
            {
                while (!IsDisposed)
                    Serve();
            } 
            else
                Serve();
        }

    }
}
