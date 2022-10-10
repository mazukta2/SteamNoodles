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
        private IViews _collection;

        public LevelsManagerMock()
        {
        }

        public void Load(string name, IViews collection)
        {
            _name = name ?? throw new Exception("name is empty");
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
            var wasFound = false;
            foreach (var item in _availableLevels)
            {
                if (item.Variation.SceneName == _name)
                {
                    wasFound = true;
                    item.LevelPrefab.Fill(_collection);
                }
            }

            if (!wasFound)
                throw new Exception($"Can't find scene with name {_name}");

            OnLoadFinished();
        }
    }
}
