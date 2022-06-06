using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Units;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Units
{
    public record UnitType : Entity
    {
        public UnitSpeed MinSpeed { get; private set; }
        public UnitSpeed Speed { get; private set; }
        public float SpeedOffset { get; private set; }

        public UnitType()
        {
            MinSpeed = new UnitSpeed(1);
            Speed = new UnitSpeed(1);
        }

        public UnitType(CustomerDefinition definition, UnitsSettingsDefinition unitsSettingsDefinition)
        {
            SpeedOffset = unitsSettingsDefinition.SpeedOffset;
            MinSpeed = new UnitSpeed(unitsSettingsDefinition.MinSpeed);
            Speed = new UnitSpeed(unitsSettingsDefinition.Speed);
        }

    }
}
