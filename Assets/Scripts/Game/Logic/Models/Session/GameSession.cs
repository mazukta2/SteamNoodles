using Assets.Scripts.Logic.Models.Levels;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Session
{
    public class GameSession
    {
        public GameSession()
        {
        }

        //public History History { get; } = new History();
        public bool IsNotLoaded => CurrentLevel == null;
        public GameLevel CurrentLevel { get; private set; }
        public SessionRandom Random { get; private set; } = new SessionRandom();

        public void SetLevel(GameLevel level)
        {
            if (CurrentLevel != null) throw new Exception("Need to unload previous level before loading new one");

            CurrentLevel = level;
        }
    }
}