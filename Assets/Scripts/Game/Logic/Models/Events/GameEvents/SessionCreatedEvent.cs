using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Session;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents
{
    public class SessionCreatedEvent : IGameEvent
    {
        public SessionCreatedEvent(GameSession session) => Session = session;
        public GameSession Session { get; }
    };
}
