using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Setups
{
    public static class UnitDefinitionSetup
    {
        public static UnitsSettingsDefinition GetDefaultUnitsDefinitions()
        {
            return new UnitsSettingsDefinition()
            {
                Speed = 1,
                SpeedUp = 1,
                SpeedUpDistance = 1,
                MinSpeed = 1,
                UnitSize = 1,
                Hairs = new string[]
                   {
                    "hair1"
                   }
            };
        }
    }
}
