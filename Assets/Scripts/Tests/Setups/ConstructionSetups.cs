using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
