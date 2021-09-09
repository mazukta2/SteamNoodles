using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Models.Session;
using Assets.Scripts.Logic.Prototypes.Levels;
using System;

namespace Assets.Scripts.Logic
{

    public class GameLogic
    {
        private GameSession _session;

        public GameLogic()
        {
        }

        public GameSession CreateSession()
        {
            if (_session != null) throw new Exception("Need to finish previous session before loading new one");
            _session = new GameSession();
            return _session;
        }
    }

}
