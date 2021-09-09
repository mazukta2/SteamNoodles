using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Events;
using System;

namespace Assets.Scripts.Logic.Models.Session
{
    public class GameSession
    {
        private GameLevel _currentLevel;

        public GameSession()
        {
        }

        public History History { get; } = new History();

        public GameLevel LoadLevel(ILevelPrototype prototype)
        {
            if (_currentLevel != null) throw new Exception("Need to unload previous level before loading new one");

            _currentLevel = new GameLevel(prototype);
            History.Add(new LevelCreatedEvent(_currentLevel));
            return _currentLevel;
        }

    }
}