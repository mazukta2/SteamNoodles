using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Services.Session;

namespace Game.Assets.Scripts.Game.Logic.Services.Units.QueueAnimations
{
    public class ServeAllFromQueue : BaseSequenceStep
    {
        private readonly UnitsCustomerQueueService _queue;
        private readonly float _delay;
        private readonly UnitsCrowdService _crowd;
        private readonly IGameRandom _random;
        private readonly Action<UnitEntity> _serve;
        private readonly TimeUpdater _updater;

        public ServeAllFromQueue(UnitsCustomerQueueService queue, UnitsCrowdService crowd, IGameRandom random, Action<UnitEntity> serve, IGameTime time, float delay)
        {
            _queue = queue;
            _delay = delay;
            _crowd = crowd;
            _random = random;
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
            if (_queue.GetUnits().Count == 0)
            {
                FireOnFinished();
                return;
            }

            var first = _queue.GetUnits().First();
            _serve(first);
            _crowd.SendToCrowd(first, _random.GetRandom() ? UnitsCrowdService.CrowdDirection.Right : UnitsCrowdService.CrowdDirection.Left);

            if (_queue.GetUnits().Count == 0)
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
