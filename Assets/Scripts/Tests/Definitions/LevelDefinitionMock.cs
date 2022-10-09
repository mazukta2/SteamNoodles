using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;

namespace Game.Assets.Scripts.Tests.Definitions
{
    public class LevelDefinitionMock : LevelDefinition
    {
        public string Name { get; set; } = "DebugLevel";
        public ViewCollectionPrefabMock LevelPrefab { get; private set; }

        public MainLevelVariation MainLevelVariation { get;  }

        public LevelDefinitionMock(string name, ViewCollectionPrefabMock level, MainLevelVariation variation)
        {
            Name = name;
            LevelPrefab = level;
            MainLevelVariation = variation;
            Variation = MainLevelVariation;
        }

    }
}
