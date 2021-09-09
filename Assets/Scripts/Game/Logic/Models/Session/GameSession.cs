using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Events;
using System;

namespace Assets.Scripts.Logic.Models.Session
{
    public class GameSession
    {
        public GameSession()
        {
        }

        public History History { get; } = new History();
        public bool IsLoading { get; private set; }
        public GameLevel CurrentLevel { get; private set; }

        public void LoadLevel(ILevelPrototype prototype)
        {
            if (CurrentLevel != null) throw new Exception("Need to unload previous level before loading new one");
            if (IsLoading) throw new Exception("Is currently loading");
            
            IsLoading = true;
           
            prototype.Load(OnFinished);
        }

        private void OnFinished(ILevelPrototype prototype)
        {
            if (!IsLoading) throw new Exception("Is currently not loading");

            CurrentLevel = new GameLevel(prototype);
            IsLoading = false;

            History.Add(new LevelLoadedEvent(CurrentLevel));
        }
    }
}