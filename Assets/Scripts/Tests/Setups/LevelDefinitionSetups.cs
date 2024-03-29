﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Levels;
using System;
using System.Collections.Generic;
using System.Text;
using Game.Assets.Scripts.Tests.Definitions;

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
            var level = new LevelDefinitionMock(new BasicMainLevel(),
            new MainLevelVariation()
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
                Waves = 10,
                SceneName = "DebugLevel"
            });
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
            var level = new LevelDefinitionMock(new EmptyLevel(),
            new MainLevelVariation()
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
                SceneName = name
            });
            return level;
        }
    }
}
