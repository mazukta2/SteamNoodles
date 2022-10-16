using System;
using Game.Assets.Scripts.Game.Logic.Models;

namespace Game.Assets.Scripts.Game.Logic.Events
{
    public interface IEvents
    {
        public static IEvents Default { get; set; } = new EventManager();

        void Fire(IEvent evnt);
        void Subscribe(ISubscription subscription);
        void Unsubscribe(ISubscription subscription);
    }
}

