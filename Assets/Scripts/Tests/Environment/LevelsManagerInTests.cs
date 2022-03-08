using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Tests.Environment;
using Game.Tests.Mocks.Settings.Levels;
using System;
using System.Collections.Generic;

namespace Game.Tests.Controllers
{
    public class LevelsManagerInTests : ILevelsManager
    {
        private List<LevelDefinitionInTests> _availableLevels = new List<LevelDefinitionInTests>();
        private ManagerLoadingLevel _loading;
        private LevelInTests _level;

        public LevelsManagerInTests()
        {
        }

        public void Dispose()
        {
            if (_level != null)
                throw new Exception("Level unloaded");
        }

        public ILevel GetCurrentLevel()
        {
            return _level;
        }


        public void Load(ILevelDefinition prototype, Action<ILevel> onFinished)
        {
            if (_loading != null)
                throw new Exception("Already loading");

            if (_level != null)
                throw new Exception("You must unload level firstly");

            _loading = new ManagerLoadingLevel(prototype, onFinished);
        }

        public void Unload()
        {
            if (_loading != null)
                throw new Exception("Currently loading");

            if (_level == null)
                throw new Exception("You must load level firstly");

            _level = null;
        }

        public void Add(LevelDefinitionInTests levelDefinition)
        {
            _availableLevels.Add(levelDefinition);
        }

        public void FinishLoading()
        {
            if (_loading == null)
                throw new Exception("Nothing is loading");

            _level = new LevelInTests(this);
            ((LevelDefinitionInTests)(_loading.Prototype)).Creator.FillLevel(_level);
            _level.Loaded = true;
            _loading.OnFinished(_level);
            _loading = null;
        }

        private class ManagerLoadingLevel
        {
            public ManagerLoadingLevel(ILevelDefinition prototype, Action<ILevel> onFinished)
            {
                Prototype = prototype;
                OnFinished = onFinished;
            }

            public ILevelDefinition Prototype { get; private set; }
            public Action<ILevel> OnFinished { get; private set; }
        }
    }
}
