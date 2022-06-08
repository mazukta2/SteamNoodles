using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Tests.Mocks.Settings.Levels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class LevelsManagerMock : ILevelsManager
    {
        public event Action OnLoadFinished = delegate { };

        private Dictionary<Level, Action<IViewsCollection>> _availableLevels = new Dictionary<Level, Action<IViewsCollection>>();
        private Level _currentLevel;
        private IViewsCollection _collection;

        public LevelsManagerMock()
        {
        }

        public void Load(string name, IViewsCollection collection)
        {
            if (_currentLevel != null)
                throw new Exception("Already loading");

            if (!_availableLevels.Any(x => x.Key.Name == name))
                throw new Exception("No such level in dictionary");

            _currentLevel = _availableLevels.First(x => x.Key.Name == name).Key;
            _collection = collection;
        }

        public void Unload()
        {
            if (_currentLevel == null)
                throw new Exception("Currently loading");

            _currentLevel = null;
            _collection = null;
        }

        public Level Add(Level level, Action<IViewsCollection> action)
        {
            _availableLevels.Add(level, action);
            return level;
        }

        public Level Add(Level level)
        {
            _availableLevels.Add(level, (v) => { });
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
