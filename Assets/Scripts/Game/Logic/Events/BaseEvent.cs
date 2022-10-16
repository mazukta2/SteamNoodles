using System;
namespace Game.Assets.Scripts.Game.Logic.Events
{
    public abstract class BaseEvent : IEvent
    {
        private IEvents _events;

        public BaseEvent()
        {
            _events = IEvents.Default;
        }

        public void Fire()
        {
            _events.Fire(this);
        }
    }
}

