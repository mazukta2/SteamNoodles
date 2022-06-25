using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;
using Game.Assets.Scripts.Tests.Definitions;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class LevelsManagerMock : ILevelsManager
    {
        public event Action OnLoadFinished = delegate { };

        private Dictionary<string, Action<IViewsCollection>> _availableLevels = new Dictionary<string, Action<IViewsCollection>>();
        private string _currentLevel;
        private IViewsCollection _collection;

        public LevelsManagerMock()
        {
        }

        public void Load(string name, IViewsCollection collection)
        {
            if (_currentLevel != null)
                throw new Exception("Already loading");

            if (!_availableLevels.ContainsKey(name))
                throw new Exception("No such level in dictionary");

            _currentLevel = name;
            _collection = collection;
        }

        public void Unload()
        {
            if (_currentLevel == null)
                throw new Exception("Currently loading");

            _currentLevel = null;
            _collection = null;
        }

        public LevelDefinitionMock Add(LevelDefinitionMock level)
        {
            _availableLevels.Add(level.DefId.Path, level.LevelPrefab.Fill);
            return level;
        }

        public Level Add(Level level, Action<IViewsCollection> action)
        {
            _availableLevels.Add(level.SceneName, action);
            return level;
        }

        public Level Add(Level level)
        {
            _availableLevels.Add(level.SceneName, (v) => { });
            return level;
        }

        public void FinishLoading()
        {
            if (_currentLevel == null)
                throw new Exception("Nothing is loading");

            _availableLevels[_currentLevel].Invoke(_collection);

            OnLoadFinished();
        }

    }
}
