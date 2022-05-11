using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;

namespace Game.Tests.Mocks.Settings.Levels
{
    public class LevelDefinitionMock : LevelDefinition
    {
        public string Name { get; set; } = "DebugLevel";
        public ViewCollectionPrefabMock LevelPrefab { get; private set; }
        public LevelDefinitionMock(string name, ViewCollectionPrefabMock level)
        {
            Name = name;
            LevelPrefab = level;
        }

    }
}
