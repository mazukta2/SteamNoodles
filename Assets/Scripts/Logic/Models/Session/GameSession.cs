using Assets.Scripts.Models.Events;
using Assets.Scripts.Models.Levels;

namespace Assets.Scripts.Logic.Models.Session
{
    public class GameSession
    {
        private GameLevel _currentLevel;

        public GameSession()
        {
            CreateLevel();
        }

        public History History { get; } = new History();

        private void CreateLevel()
        {
           _currentLevel = new GameLevel();
           History.Add(new LevelCreatedEvent(_currentLevel));
        }

        public class LevelCreatedEvent : IGameEvent 
        {
            public LevelCreatedEvent(GameLevel level) => (Level) = (level);
            public GameLevel Level { get; } 
        };
    }
}