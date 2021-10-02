using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Assets.Scripts.Logic.Models.Session
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