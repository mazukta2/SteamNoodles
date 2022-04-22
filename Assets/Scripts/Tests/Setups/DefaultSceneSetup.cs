using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Constructions;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Levels;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Screens;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Setups
{
    public class DefaultSceneSetup
    {
        private AssetsMock _assets;
        private DefinitionsMock _definitions;

        public DefaultSceneSetup(AssetsMock assets, DefinitionsMock definitions)
        {
            _assets = assets;
            _definitions = definitions;
        }

        public void Create()
        {
            _assets.AddPrefab("Screens/MainScreen", new MainScreenPrefab());
            _assets.AddPrefab("Screens/BuildScreen", new BuildScreenPrefab());
            _definitions.Add(nameof(ConstructionsSettingsDefinition), new ConstructionsSettingsDefinition()
            {
                CellSize = 0.5f,
            });
            _assets.AddPrefab("DebugConstruction", new BasicConstructionModelPrefab());
            _definitions.Add(nameof(UnitsSettingsDefinition), new UnitsSettingsDefinition()
            {
                Speed = 1,
                UnitSize = 1,
                Hairs = new string[]
                {
                    "hair1"
                }
            });

            var construciton = new ConstructionDefinition()
            {
                Name = "Name",
                Placement = new int[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                },
                LevelViewPath = "DebugConstruction",
                Points = 1,
            };
            _definitions.Add("Construction1", construciton);

            var customer = new CustomerDefinition()
            {
                ConstructionsReward = new Dictionary<ConstructionDefinition, int>()
                {
                    { construciton, 1}
                }
            };
            _definitions.Add("Customer1", customer);

            var fields = new List<PlacementFieldDefinition>();
            fields.Add(new PlacementFieldDefinition()
            {
                Size = new IntPoint(9, 9)
            });
            var level = new LevelDefinitionMock("DebugLevel", new BasicSellingLevel())
            {
                HandSize = 5,
                PlacementFields = fields,
                StartingHand = new List<ConstructionDefinition>() { construciton },
                CrowdUnitsAmount = 10,
                BaseCrowdUnits = new Dictionary<CustomerDefinition, int>() {
                    { customer, 2 }
                },
                UnitsRect = new FloatRect(-10, -10, 20, 20),
            };
            _definitions.Add(level.Name, level);
        }

    }
}