namespace Game.Assets.Scripts.Tests.Managers.Game
{
    /*
    public class TestGameConstructor
    {
        private TestGameEngine _engine = new TestGameEngine();
        private TestLevelDefinition _loadLevel;

        public TestGameBuild Build()
        {
            var core = new Core(_engine);
            var build = new TestGameBuild(core, _engine);
            if (_loadLevel != null)
                build.LoadLevel(_loadLevel);

            return build;
        }

        public TestGameConstructor LoadDefinitions(DefinitionsMockCreator definitions)
        {
            definitions.Create(_engine);
            return this;
        }

        public TestGameConstructor AddLevel(TestLevelDefinition levelDefinition)
        {
            _engine.Levels.Add(levelDefinition);
            return this;
        }

        public TestGameConstructor LoadLevel(LevelMockCreator mockCreator)
        {
            var levelDefinition = new TestLevelDefinition(mockCreator);
            AddLevel(levelDefinition);
            _loadLevel = levelDefinition;
            return this;
        }
    }
    */
}
