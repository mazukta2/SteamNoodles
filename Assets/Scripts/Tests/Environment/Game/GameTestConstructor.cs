using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using System;

namespace Game.Assets.Scripts.Tests.Managers.Game
{
    public class GameTestConstructor
    {
        private GameEngineInTests _engine = new GameEngineInTests();
        private LevelDefinitionInTests _loadLevel;

        public GameTestConstructor()
        {
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

        public GameTestConstructor LoadDefinitions(DefinitionsMockCreator definitions)
        {
            definitions.Create(_engine);
            return this;
        }

        public GameTestConstructor AddLevel(LevelDefinitionInTests levelDefinition)
        {
            _engine.Levels.Add(levelDefinition);
            return this;
        }

        public GameTestConstructor AddAndLoadLevel(LevelMockCreator mockCreator)
        {
            var levelDefinition = new LevelDefinitionInTests(mockCreator);
            AddLevel(levelDefinition);
            _loadLevel = levelDefinition;
            return this;
        }

        public GameTestConstructor AddAndLoadLevel(LevelDefinitionInTests levelDefinition)
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

    }
}
