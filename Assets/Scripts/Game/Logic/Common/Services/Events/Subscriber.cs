using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using System;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Events
{
    public class Subscriber<T> : ISubscriber where T : IEvent
    {
        private readonly EventManager _manager;
        private readonly Action<T> _handler;

        public Subscriber(EventManager manager, Action<T> handler)
        {
            _manager = manager;
            _handler = handler;
        }

        public void Fire(T evnt)
        {
            _handler(evnt);
        }

        public void Dispose()
        {
            _manager.Remove(this);
        }
    }

    public interface ISubscriber
    {

    }
}
