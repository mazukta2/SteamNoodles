using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Tests.Definitions;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class LevelsManagerMock : ILevelsManager
    {
        public event Action OnLoadFinished = delegate { };

        private List<LevelDefinitionMock> _availableLevels = new List<LevelDefinitionMock>();
        private string _name;
        private IViewsCollection _collection;

        public LevelsManagerMock()
        {
        }

        public void Load(string name, IViewsCollection collection)
        {
            _name = name;
            _collection = collection;
        }

        public void Unload()
        {
            _name = null;
            _collection = null;
        }

        public void Add(LevelDefinitionMock levelDefinition)
        {
            _availableLevels.Add(levelDefinition);
        }

        public void FinishLoading()
        {
            foreach (var item in _availableLevels)
            {
                if (item.Variation.SceneName == _name)
                    item.LevelPrefab.Fill(_collection);
            }

            OnLoadFinished();
        }
    }
}
