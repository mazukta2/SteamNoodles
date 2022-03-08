using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using System;

namespace Game.Tests.Mocks.Settings.Levels
{
    public class LevelDefinitionInTests : ILevelDefinition
    {
        public string SceneName => throw new NotImplementedException();
        public LevelMockCreator Creator { get; private set; }
        public LevelDefinitionInTests(LevelMockCreator creator)
        {
            Creator = creator;
        }
    }
}
