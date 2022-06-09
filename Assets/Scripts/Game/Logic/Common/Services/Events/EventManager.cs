using Game.Assets.Scripts.Game.Logic.Common.Services.Events;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Commands
{
    public class EventManager : IEvents
    {
        private List<ISubscriber> _list = new List<ISubscriber>();

        public void Execute<T>(T evnt) where T : IEvent
        {
            foreach (var handler in _list)
            {
                if (handler is Subscriber<T> subscriber)
                    subscriber.Fire(evnt);
            }
        }

        public Subscriber<T> Get<T>(Action<T> handler) where T : IEvent
        {
            var sub = new Subscriber<T>(this, handler);
            _list.Add(sub);
            return sub;
        }

        public void Remove<T>(Subscriber<T> subscriber) where T : IEvent
        {
            _list.Remove(subscriber);
        }
    }
}
