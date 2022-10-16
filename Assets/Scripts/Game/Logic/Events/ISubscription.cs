using System;
namespace Game.Assets.Scripts.Game.Logic.Events
{
    public interface ISubscription
    {
        void Fire(IEvent evnt);
    }
}

