using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Tests.Setups;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using System;

namespace Game.Assets.Scripts.Tests.Environment.Game
{
    public class GameConstructor
    {
        private bool _disableAutoload;
        private DefinitionsMock _definitions = new DefinitionsMock();
        private AssetsMock _assets = new AssetsMock();
        private LevelsManagerMock _levelsManager = new LevelsManagerMock();
        private DefaultSceneSetup _setup;

        public GameConstructor()
        {
            _setup = new DefaultSceneSetup(_assets, _definitions);
            _setup.Create();
        }

        public TestsGameBuild Build()
        {
            var build = new TestsGameBuild(_assets, _definitions, _levelsManager);
            if (!_disableAutoload)
            {
                build.LoadLevel(IGameDefinitions.Default.Get<LevelDefinitionMock>("DebugLevel"));
            }

            return build;
        }

        public GameConstructor AddDefinition(string name, object obj)
        {
            _definitions.Add(name, obj);
            return this;
        }

        public GameConstructor AddLevel(LevelDefinitionMock levelDefinition)
        {
            _levelsManager.Add(levelDefinition);
            _definitions.Add(levelDefinition.Name, levelDefinition);
            return this;
        }

        public GameConstructor DisableAutoLoad()
        {
            _disableAutoload = true;
            return this;
        }

        public GameConstructor UpdateDefinition<T>(Action<T> p)
        {
            p(_definitions.Get<T>());
            return this;
        }
    }
}
