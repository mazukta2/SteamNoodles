﻿using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;

namespace Game.Assets.Scripts.Tests.Definitions
{
    public class LevelDefinitionMock : LevelDefinition
    {
        public ViewCollectionPrefabMock LevelPrefab { get; private set; }

        public MainLevelVariation LevelSettings { get;  }

        public LevelDefinitionMock(ViewCollectionPrefabMock level, MainLevelVariation variation)
        {
            LevelPrefab = level;
            LevelSettings = variation;
            Variation = LevelSettings;
        }

    }
}
