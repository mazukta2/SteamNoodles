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
        private LevelDefinition _definition;
        private IViewsCollection _collection;

        public LevelsManagerMock()
        {
        }

        public void Load(LevelDefinition prototype, IViewsCollection collection)
        {
            if (_definition != null)
                throw new Exception("Already loading");

            _definition = prototype;
            _collection = collection;
        }

        public void Unload()
        {
            if (_definition == null)
                throw new Exception("Currently loading");

            _definition = null;
            _collection = null;
        }

        public void Add(LevelDefinitionMock levelDefinition)
        {
            _availableLevels.Add(levelDefinition);
        }

        public void FinishLoading()
        {
            if (_definition == null)
                throw new Exception("Nothing is loading");

            ((LevelDefinitionMock)_definition).LevelPrefab.Fill(_collection);

            OnLoadFinished();
        }
    }
}
