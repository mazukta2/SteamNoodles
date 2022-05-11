using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Levels;
using Game.Tests.Mocks.Settings.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Setups
{
    public class LevelDefinitionSetups
    {
        public static LevelDefinitionMock GetDefault()
        {
            var construciton = ConstructionSetups.GetDefault();
            var customer = new CustomerDefinition()
            {
            };
            return GetDefault(customer, construciton);
        }

        public static LevelDefinitionMock GetDefault(CustomerDefinition customer, ConstructionDefinition construciton)
        {
            var fields = new PlacementFieldDefinition()
            {
                Size = new IntPoint(9, 9)
            };
            var level = new LevelDefinitionMock("DebugLevel", new BasicSellingLevel())
            {
                PlacementField = fields,
                StartingHand = new List<ConstructionDefinition>() { construciton },
                CrowdUnitsAmount = 10,
                BaseCrowdUnits = new Dictionary<CustomerDefinition, int>() {
                    { customer, 2 }
                },
                UnitsRect = new FloatRect(-10, -10, 20, 20),
                ConstructionsReward = new Dictionary<ConstructionDefinition, int>()
                {
                    { construciton, 1}
                },
                Starter = new MainLevelStarter()
            };
            return level;
        }

        public static LevelDefinitionMock GetEmpty(string name)
        {
            var construciton = ConstructionSetups.GetDefault();
            var customer = new CustomerDefinition()
            {
            };
            var fields = new PlacementFieldDefinition()
            {
                Size = new IntPoint(9, 9)
            };
            var level = new LevelDefinitionMock(name, new EmptyLevel())
            {
                PlacementField = fields,
                StartingHand = new List<ConstructionDefinition>() { construciton },
                CrowdUnitsAmount = 10,
                BaseCrowdUnits = new Dictionary<CustomerDefinition, int>() {
                    { customer, 2 }
                },
                UnitsRect = new FloatRect(-10, -10, 20, 20),
                ConstructionsReward = new Dictionary<ConstructionDefinition, int>()
                {
                    { construciton, 1}
                },
                Starter = new EmptyLevelStarter()
            };
            return level;
        }
    }
}
