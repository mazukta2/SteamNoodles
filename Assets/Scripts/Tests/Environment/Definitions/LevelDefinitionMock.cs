using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Tests.Mocks.Levels;

namespace Game.Tests.Mocks.Settings.Levels
{
    public class LevelDefinitionMock : LevelDefinition
    {
        public LevelPrefabMock LevelPrefab { get; private set; }
        public LevelDefinitionMock(LevelPrefabMock level)
        {
            LevelPrefab = level;
        }

    }
}
