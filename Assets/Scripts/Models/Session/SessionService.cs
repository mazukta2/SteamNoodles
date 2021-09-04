using Assets.Scripts.Data;
using Assets.Scripts.Models.Events;
using Assets.Scripts.Models.Levels;
using Assets.Scripts.Models.Services;

namespace Assets.Scripts.Models.Session
{
    public class SessionService : IService
    {
        private IServiceManager _services;
        private GameSessionData _data;
        private GameLevel _currentLevel;

        public SessionService(IServiceManager services, GameSessionData gameSessionData)
        {
            _services = services;
            _data = gameSessionData;
            CreateLevel();
        }
        public History History { get; } = new History();

        private void CreateLevel()
        {
            _currentLevel = new GameLevel(_data.Buildings);
            History.Add(new LevelCreatedEvent(_currentLevel));
        }

        public class LevelCreatedEvent : IGameEvent 
        {
            public LevelCreatedEvent(GameLevel level) => (Level) = (level);
            public GameLevel Level { get; } 
        };
    }
}
