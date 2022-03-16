using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using System.Collections.Generic;

namespace Game.Tests.Mocks.Settings.Levels
{
    public class LevelDefinitionMock : LevelDefinition
    {
        public string Name { get; set; } = "DebugLevel";
        public LevelPrefabMock LevelPrefab { get; private set; }
        public LevelDefinitionMock(LevelPrefabMock level)
        {
            LevelPrefab = level;
            var fields = new List<PlacementFieldDefinition>();
            fields.Add(new PlacementFieldDefinition()
            {
                Size = new IntPoint(10, 10)
            });
            PlacementFields = fields;
        }

    }
}
