using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Session;
using Assets.Scripts.Models.Events;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public class GameLogic
    {
        public GameSession Session { get; private set; }

        public GameLogic()
        {
        }

        public History History { get; } = new History();
        public void CreateSession()
        {
            if (Session != null) throw new Exception("Need to finish previous session before loading new one");
            Session = new GameSession();

            History.Add(new SessionCreatedEvent(Session));
        }
    }

}
