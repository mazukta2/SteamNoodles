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
        public float SpeedUp { get; private set; }
        public float SpeedUpDistance { get; private set; }
        public float SpeedOffset { get; private set; }
        public float RotationSpeed { get; private set; }
        public string[] Hairs { get; private set; }
        public int BaseCoins { get; private set; }

        public UnitType(float speedOffset = 0,
            float rotationSpeed = 1, float speedUp = 1, float speedUpDistance = 0, int coins = 0) :
            this(new UnitSpeed(1), new UnitSpeed(1), speedOffset, rotationSpeed, speedUp, speedUpDistance, coins)
        {
        }

        public UnitType(CustomerDefinition definition, UnitsSettingsDefinition unitsSettingsDefinition)
        {
            SpeedOffset = unitsSettingsDefinition.SpeedOffset;
            MinSpeed = new UnitSpeed(unitsSettingsDefinition.MinSpeed);
            Speed = new UnitSpeed(unitsSettingsDefinition.Speed);
            RotationSpeed = unitsSettingsDefinition.RotationSpeed;
            Hairs = unitsSettingsDefinition.Hairs;
            SpeedUp = unitsSettingsDefinition.SpeedUp;
            SpeedUpDistance = unitsSettingsDefinition.SpeedUpDistance;
            BaseCoins = unitsSettingsDefinition.BaseCoins;
        }

        public UnitType(UnitSpeed minSpeed, UnitSpeed speed, float speedOffset = 0, 
            float rotationSpeed = 1, float speedUp = 1, float speedUpDistance = 0, int coins = 0)
        {
            SpeedOffset = speedOffset;
            MinSpeed = minSpeed;
            Speed = speed;
            RotationSpeed = rotationSpeed;
            SpeedUp = speedUp;
            SpeedUpDistance = speedUpDistance;
            BaseCoins = coins;
        }
    }
}
