using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.Animations
{
    public class ServeAllFromQueue : BaseQueueStep
    {
        private readonly CustomerQueue _queue;
        private readonly float _delay;
        private readonly ICrowd _crowd;
        private readonly Action<Unit> _removeFromQueue;
        private readonly TimeUpdater _updater;

        public ServeAllFromQueue(CustomerQueue queue, ICrowd crowd, Action<Unit> removeFromQueue, IGameTime time, float delay)
        {
            _queue = queue;
            _delay = delay;
            _crowd = crowd;
            _removeFromQueue = removeFromQueue;
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
            _crowd.SendToCrowd(first, LevelCrowd.CrowdDirection.Left);

            var list = _queue.Units.ToArray();
            for (int i = 0; i < list.Count(); i++)
            {
                list[i].SetPosition(_queue.GetPositionFor(i));
            }

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
