using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Tests.Setups;
using System;
using Game.Assets.Scripts.Tests.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Languages;

namespace Game.Assets.Scripts.Tests.Environment.Game
{
    public class BuildConstructor
    {
        private bool _disableAutoload;
        private DefinitionsMock _definitions = new DefinitionsMock();
        private AssetsMock _assets = new AssetsMock();
        private LevelsManagerMock _levelsManager = new LevelsManagerMock();
        private DefaultSceneSetup _setup;

        public BuildConstructor()
        {
            _setup = new DefaultSceneSetup(_assets, _definitions);
            _definitions.Add("English", new LanguageDefinition() { Name = "English" });
            _setup.Create();
        }

        public FakeGameBuild Build()
        {
            var build = new FakeGameBuild(_assets, _definitions, _levelsManager, _disableAutoload);
            return build;
        }

        public BuildConstructor AddDefinition(string name, object obj)
        {
            _definitions.Add(name, obj);
            return this;
        }

        public BuildConstructor AddLevel(LevelDefinitionMock levelDefinition)
        {
            _levelsManager.Add(levelDefinition);
            _definitions.Add(levelDefinition.Variation.SceneName, levelDefinition);
            return this;
        }

        public BuildConstructor DisableAutoLoad()
        {
            _disableAutoload = true;
            return this;
        }

        public BuildConstructor UpdateDefinition<T>(Action<T> p)
        {
            p(_definitions.Get<T>());
            return this;
        }
    }
}
