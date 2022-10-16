using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;

namespace Game.Assets.Scripts.Game.Logic.Events
{
    public class Subscription<T> : Disposable, ISubscription where T: IEvent
    {
        private IEvents _events;
        private Action _action;

        public Subscription(IEvents events, Action action)
        {
            _events = events;
            _action = action;

            _events.Subscribe(this);
        }

        protected override void DisposeInner()
        {
            _events.Unsubscribe(this);
        }

        public void Fire(IEvent evnt)
        {
            if (evnt is T)
            {
                _action();
            }
        }
    }
}

