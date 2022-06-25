using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;

namespace Game.Assets.Scripts.Game.Logic.Entities.Levels
{
    public record Level : Entity
    {
        public string SceneName { get; private set; }

        private Action<Level> _startServices;
        private Action<Level> _startLevel;

        public Level()
        {
            SceneName = "not_impotant";
            _startServices = (l) => { };
            _startLevel = (l) => { };
        }

        public Level(LevelDefinition levelDefinition)
        {
            SceneName = levelDefinition.SceneName;

            if (levelDefinition.Starter == null)
            {
                _startServices = (l) => { };
                _startLevel = (l) => { };
            }
            else
            {
                _startServices = levelDefinition.Starter.StartServices;
                _startLevel = levelDefinition.Starter.StartLevel;
            }
        }

        public void StartServices()
        {
            _startServices(this);
        }

        public void StartLevel()
        {
            _startLevel(this);
        }
    }
}
