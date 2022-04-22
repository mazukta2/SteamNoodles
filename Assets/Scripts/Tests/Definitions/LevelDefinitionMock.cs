using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Tests.Mocks.Levels;

namespace Game.Tests.Mocks.Settings.Levels
{
    public class LevelDefinitionMock : LevelDefinition
    {
        public string Name { get; set; } = "DebugLevel";
        public LevelPrefabMock LevelPrefab { get; private set; }
        public LevelDefinitionMock(string name, LevelPrefabMock level)
        {
            Name = name;
            LevelPrefab = level;
        }

    }
}
