﻿using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units.QueueAnimations
{
    public class ServeAllFromQueue : BaseSequenceStep
    {
        private readonly UnitsCustomerQueueService _queue;
        private readonly float _delay;
        private readonly UnitsCrowdService _crowd;
        private readonly IGameRandom _random;
        private readonly Action<Unit> _removeFromQueue;
        private readonly Action<Unit> _serve;
        private readonly TimeUpdater _updater;

        public ServeAllFromQueue(UnitsCustomerQueueService queue, UnitsCrowdService crowd, IGameRandom random, Action<Unit> removeFromQueue, Action<Unit> serve, IGameTime time, float delay)
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
            if (_queue.GetUnits().Count == 0)
            {
                FireOnFinished();
                return;
            }

            var first = _queue.GetUnits().First();
            _removeFromQueue(first);
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