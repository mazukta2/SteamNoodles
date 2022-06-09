using System;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Events
{
    public interface IEvents
    {
        public static IEvents Default { get; set; }

        public void Execute<T>(T evt) where T : IEvent;
        public Subscriber<T> Get<T>(Action<T> handler) where T : IEvent;
    }
}
