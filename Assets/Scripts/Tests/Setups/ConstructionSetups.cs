using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using System;
using System.Collections.Generic;
using System.Text;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Tests.Setups
{
    public class ConstructionSetups
    {
        public static ConstructionDefinition GetDefault()
        {
            var construciton = new ConstructionDefinition()
            {
                DefId = new DefId("Construction"),
                Name = "Name",
                Placement = new int[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                },
                LevelViewPath = "DebugConstruction",
                Points = 1,
            };
            return construciton;
        }

        public static ConstructionScheme GetDefaultScheme()
        {
            var placement = new ContructionPlacement(new int[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                });
            var scheme = new ConstructionScheme(new Uid(),
                new DefId("Construction"),
                placement,
                LocalizationTag.None,
                new BuildingPoints(1),
                new AdjacencyBonuses(),
                "DebugConstruction", "DebugConstruction", new Requirements());
            return scheme;
        }
    }
}
