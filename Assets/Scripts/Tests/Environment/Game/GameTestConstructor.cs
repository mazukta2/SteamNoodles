using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Tests.Environment.Definitions.List;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using System;
using System.Collections.Generic;
using System.IO;

namespace Game.Assets.Scripts.Tests.Managers.Game
{
    public class GameTestConstructor
    {
        private GameEngineInTests _engine = new GameEngineInTests();
        private LevelDefinitionMock _loadLevel;

        public GameTestConstructor()
        {
            LoadDefinitions(new DefaultDefinitions());
            _loadLevel = _engine.Settings.Get<LevelDefinitionMock>("DebugLevel");
        }

        public GameTestConstructor(BaseConstructorSettings settings)
        {
            settings.Fill(this);
        }

        public GameTestBuild Build()
        {
            var core = new Core(_engine);

            var build = new GameTestBuild(core, _engine);
            if (_loadLevel != null)
                build.LoadLevel(_loadLevel);

            return build;
        }

        public GameTestConstructor ClearLoading()
        {
            _loadLevel = null;
            return this;
        }

        public GameTestConstructor AddDefinition(string name, object obj)
        {
            _engine.Settings.Add(name, obj);
            return this;
        }

        public GameTestConstructor LoadDefinitions(DefinitionsMockCreator definitions)
        {
            definitions.Create(_engine);
            return this;
        }

        public GameTestConstructor AddLevel(LevelDefinitionMock levelDefinition)
        {
            _engine.Levels.Add(levelDefinition);
            _engine.Settings.Add(levelDefinition.Name, levelDefinition);
            return this;
        }

        public GameTestConstructor AddAndLoadLevel(LevelPrefabMock mockCreator)
        {
            var levelDefinition = new LevelDefinitionMock("lvl", mockCreator);
            AddLevel(levelDefinition);
            _loadLevel = levelDefinition;
            return this;
        }

        public GameTestConstructor AddAndLoadLevel(LevelDefinitionMock levelDefinition)
        {
            AddLevel(levelDefinition);
            _loadLevel = levelDefinition;
            return this;
        }

        public GameTestConstructor UpdateDefinition<T>(Action<T> p)
        {
            p(_engine.Settings.Get<T>());
            return this;
        }

        public T GetDefintiion<T>()
        {
            return _engine.Settings.Get<T>();
        }
    }
}
