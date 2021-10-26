using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents;
using Game.Assets.Scripts.Game.Logic.Models.Session;
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
