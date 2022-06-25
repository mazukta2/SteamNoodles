using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;

namespace Game.Assets.Scripts.Tests.Definitions
{
    public class LevelDefinitionMock : LevelDefinition
    {
        public ViewCollectionPrefabMock LevelPrefab { get; private set; }
        public LevelDefinitionMock(string name, ViewCollectionPrefabMock level)
        {
            DefId = new DefId(name);
            LevelPrefab = level;
        }

    }
}
