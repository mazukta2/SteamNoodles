using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Tests.Mocks.Settings.Levels;
using System;
using System.Collections.Generic;

namespace Game.Tests.Controllers
{
    public class LevelsManagerInTests : ILevelsManager
    {
        private List<LevelDefinitionMock> _availableLevels = new List<LevelDefinitionMock>();
        private ManagerLoadingLevel _loading;
        private LevelView _level;
        private IGameEngine _engine;

        public LevelsManagerInTests(IGameEngine engine)
        {
            _engine = engine;
        }

        public void Dispose()
        {
            if (_level != null)
            {
                _level.Dispose();
            }
        }

        public LevelView GetCurrentLevel()
        {
            return _level;
        }


        public void Load(GameLevel model, LevelDefinition prototype, Action<LevelView> onFinished)
        {
            if (_loading != null)
                throw new Exception("Already loading");

            if (_level != null)
                throw new Exception("You must unload level firstly");

            _loading = new ManagerLoadingLevel(model, prototype, onFinished);
        }

        public void Unload()
        {
            if (_loading != null)
                throw new Exception("Currently loading");

            if (_level == null)
                throw new Exception("You must load level firstly");

            _level = null;
        }

        public void Add(LevelDefinitionMock levelDefinition)
        {
            _availableLevels.Add(levelDefinition);
        }

        public void FinishLoading()
        {
            if (_loading == null)
                throw new Exception("Nothing is loading");

            _level = new LevelView(_engine, _loading.Model);
            ((LevelDefinitionMock)(_loading.Prototype)).LevelPrefab.FillLevel(_level);
            _level.FinishLoading();
            _loading.OnFinished(_level);
            _loading = null;
        }

        private class ManagerLoadingLevel
        {
            public ManagerLoadingLevel(GameLevel model, LevelDefinition prototype, Action<LevelView> onFinished)
            {
                Prototype = prototype;
                OnFinished = onFinished;
                Model = model;
            }
             
            public GameLevel Model { get; private set; }
            public LevelDefinition Prototype { get; private set; }
            public Action<LevelView> OnFinished { get; private set; }
        }
    }
}
