using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Prefabs.Levels.Constructions;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment.Definitions.List
{
    public class DefaultDefinitions : DefinitionsMockCreator
    {
        public override void Create(GameEngineInTests engine)
        {
            engine.Assets.Screens.AddPrototype<MainScreenView>(new MainScreenPrefab());
            engine.Assets.Screens.AddPrototype<BuildScreenView>(new BuildScreenPrefab());
            engine.Settings.Add(nameof(ConstructionsSettingsDefinition), new ConstructionsSettingsDefinition() { 
                CellSize = 0.5f,
            });
            engine.Assets.AddPrefab("DebugConstruction", new BasicConstructionModelPrefab());
            engine.Settings.Add(nameof(UnitsSettingsDefinition), new UnitsSettingsDefinition()
            {
                Speed = 1,
            });
            
            var construciton = new ConstructionDefinition()
            {
                Placement = new int[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                },
                LevelViewPath = "DebugConstruction",
                Points = 1,
            };
            engine.Settings.Add("Construction1", construciton);

            var customer = new CustomerDefinition()
            {
                ConstructionsReward = new Dictionary<ConstructionDefinition, int>()
                {
                    { construciton, 1}
                }
            };
            engine.Settings.Add("Customer1", customer);

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
            engine.Settings.Add(level.Name, level);
        }

    }
}