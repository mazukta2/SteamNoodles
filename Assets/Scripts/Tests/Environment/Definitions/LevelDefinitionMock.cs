using Assets.Scripts.Logic.Prototypes.Levels;
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
        public LevelDefinitionMock(string name, LevelPrefabMock level)
        {
            Name = name;
            LevelPrefab = level;
            var fields = new List<PlacementFieldDefinition>();
            fields.Add(new PlacementFieldDefinition()
            {
                Size = new IntPoint(9, 9)
            });
            PlacementFields = fields;
            CrowdUnitsAmount = 10;
            BaseCrowdUnits = new Dictionary<CustomerDefinition, int>() {
                { new CustomerDefinition() , 2 } 
            };
            UnitsRect = new FloatRect(-10, -10, 20, 20);
        }

    }
}
