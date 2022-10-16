using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Events
{
    public class EventManager : IEvents
    {
        private List<ISubscription> _list = new List<ISubscription>();

        public void Fire(IEvent evnt)
        {
            foreach (var item in _list)
            {
                item.Fire(evnt);
            }
        }

        public void Subscribe(ISubscription subscription)
        {
            _list.Add(subscription);
        }

        public void Unsubscribe(ISubscription subscription)
        {
            _list.Remove(subscription);
        }
    }
}

