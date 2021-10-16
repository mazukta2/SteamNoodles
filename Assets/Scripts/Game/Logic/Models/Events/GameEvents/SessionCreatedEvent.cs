using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic.Models.Events.GameEvents
{
    public class SessionCreatedEvent : IGameEvent
    {
        public SessionCreatedEvent(GameSession session) => (Session) = (session);
        public GameSession Session { get; }
    };
}
