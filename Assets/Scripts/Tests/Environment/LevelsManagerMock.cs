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
        private ManagerLoadingLevel _loading;

        public LevelController Controller { get; private set; }

        public IViewsCollection Collection => Controller.Collection;

        public LevelsManagerMock()
        {
        }

        public void Dispose()
        {
            Controller.Dispose();
        }

        public void Load(GameLevel model, LevelDefinition prototype, Action<IViewsCollection> onFinished)
        {
            if (_loading != null)
                throw new Exception("Already loading");

            if (Controller != null)
                throw new Exception("You must unload level firstly");

            _loading = new ManagerLoadingLevel(model, prototype, onFinished);
        }

        public void Unload()
        {
            if (_loading != null)
                throw new Exception("Currently loading");

            if (Controller == null)
                throw new Exception("You must load level firstly");

            Controller.Dispose();
            Controller = null;
            ICurrentLevel.Default = null;
        }

        public void Add(LevelDefinitionMock levelDefinition)
        {
            _availableLevels.Add(levelDefinition);
        }

        public void FinishLoading()
        {
            if (_loading == null)
                throw new Exception("Nothing is loading");

            ICurrentLevel.Default = _loading.Model;
            Controller = new LevelController(_loading.Model.Definition);
            ((LevelDefinitionMock)_loading.Prototype).LevelPrefab.Fill(Controller.Collection);

            Controller.Initialize();
            _loading.OnFinished(Controller.Collection);
            _loading = null;

            Controller.Start();
        }

        private class ManagerLoadingLevel
        {
            public ManagerLoadingLevel(GameLevel model, LevelDefinition prototype, Action<IViewsCollection> onFinished)
            {
                Prototype = prototype;
                OnFinished = onFinished;
                Model = model;
            }

            public GameLevel Model { get; private set; }
            public LevelDefinition Prototype { get; private set; }
            public Action<IViewsCollection> OnFinished { get; private set; }
        }
    }
}
