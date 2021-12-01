using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public class GameLogic : Disposable
    {
        public event Action OnSessionCreated = delegate { };
        public GameSession Session { get; private set; }

        public GameLogic()
        {
        }
        
        protected override void OnDispose()
        {
        }

        public void CreateSession()
        {
            if (Session != null) throw new Exception("Need to finish previous session before loading new one");
            Session = new GameSession();
            OnSessionCreated();
        }

    }

}
