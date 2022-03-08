using Game.Assets.Scripts.Tests.Mocks.Levels;

namespace Game.Assets.Scripts.Tests.Managers.Game
{
    internal class LevelDefinitionInTest
    {
        private LevelMockCreator _mockCreator;

        public LevelDefinitionInTest(LevelMockCreator mockCreator)
        {
            _mockCreator = mockCreator;
        }
    }
}