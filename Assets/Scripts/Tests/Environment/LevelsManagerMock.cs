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
        private ILevel _model;
        private IViewsCollection _collection;

        public LevelsManagerMock()
        {
        }

        public void Load(ILevel prototype, IViewsCollection collection)
        {
            if (_model != null)
                throw new Exception("Already loading");

            _model = prototype;
            _collection = collection;
        }

        public void Unload()
        {
            if (_model == null)
                throw new Exception("Currently loading");

            _model = null;
            _collection = null;
        }

        public void Add(LevelDefinitionMock levelDefinition)
        {
            _availableLevels.Add(levelDefinition);
        }

        public void FinishLoading()
        {
            if (_model == null)
                throw new Exception("Nothing is loading");

            foreach (var item in _availableLevels)
            {
                if (item.Variation.SceneName == _model.SceneName)
                    item.LevelPrefab.Fill(_collection);
            }

            OnLoadFinished();
        }
    }
}
