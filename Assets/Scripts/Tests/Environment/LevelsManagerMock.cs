using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Tests.Mocks.Settings.Levels;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class LevelsManagerMock : ILevelsManager
    {
        private List<LevelDefinitionMock> _availableLevels = new List<LevelDefinitionMock>();
        private LoadData _loading;
        private IViewsCollection _viewsCollection;

        public LevelsManagerMock()
        {
        }

        public void Load(LevelDefinition prototype, Action<IViewsCollection> onFinished)
        {
            if (_loading != null)
                throw new Exception("Already loading");

            _loading = new LoadData(prototype, onFinished);
        }

        public void Unload()
        {
            if (_loading != null)
                throw new Exception("Currently loading");

            _viewsCollection.Dispose();
            _viewsCollection = null;
        }

        public void Add(LevelDefinitionMock levelDefinition)
        {
            _availableLevels.Add(levelDefinition);
        }

        public void FinishLoading()
        {
            if (_loading == null)
                throw new Exception("Nothing is loading");

            _viewsCollection = new ViewsCollection();
            ((LevelDefinitionMock)_loading.Prototype).LevelPrefab.Fill(_viewsCollection);

            var lvl = _loading;
            _loading = null;
            lvl.OnFinished(_viewsCollection);
        }

        private class LoadData
        {
            public LoadData(LevelDefinition prototype, Action<IViewsCollection> onFinished)
            {
                Prototype = prototype;
                OnFinished = onFinished;
            }

            public LevelDefinition Prototype { get; private set; }
            public Action<IViewsCollection> OnFinished { get; private set; }
        }
    }
}
