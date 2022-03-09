using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Tests.Mocks.Levels;

namespace Game.Tests.Mocks.Settings.Levels
{
    public class LevelDefinitionInTests : LevelDefinition
    {
        public LevelMockCreator Creator { get; private set; }
        public LevelDefinitionInTests(LevelMockCreator creator)
        {
            Creator = creator;
        }

    }
}
