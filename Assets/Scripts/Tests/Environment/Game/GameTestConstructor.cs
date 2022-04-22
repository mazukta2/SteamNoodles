using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using System;

namespace Game.Assets.Scripts.Tests.Managers.Game
{
    public class GameTestConstructor
    {
        private GameEngineInTests _engine = new GameEngineInTests();
        private bool _disableAutoload;
        private DefinitionsMock _definitions = new DefinitionsMock();
        private AssetsMock _assets = new AssetsMock();
        private DefaultSceneSetup _setup;

        public GameTestConstructor()
        {
            _setup = new DefaultSceneSetup(_assets, _definitions);
            _setup.Create();
        }

        public GameBuildMock Build()
        {
            var core = new Core(_engine);

            var build = new GameBuildMock(core, _engine, _assets, _definitions);
            if (!_disableAutoload)
            {
                build.LoadLevel(IDefinitions.Default.Get<LevelDefinitionMock>("DebugLevel"));
            }

            return build;
        }

        public GameTestConstructor AddDefinition(string name, object obj)
        {
            _definitions.Add(name, obj);
            return this;
        }

        public GameTestConstructor AddLevel(LevelDefinitionMock levelDefinition)
        {
            _engine.Levels.Add(levelDefinition);
            _definitions.Add(levelDefinition.Name, levelDefinition);
            return this;
        }

        public GameTestConstructor DisableAutoLoad() 
        {
            _disableAutoload = true;
            return this;
        }

        public GameTestConstructor UpdateDefinition<T>(Action<T> p)
        {
            p(_definitions.Get<T>());
            return this;
        }
    }
}
