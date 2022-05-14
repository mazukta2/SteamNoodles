using System;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Constructions
{
    public class UnitsSettingsDefinition
    {
        public float Speed { get; set; }
        public float SpeedOffset { get; set; }
        public float UnitSize { get; set; }
        public string[] Hairs { get; set; }
        public float RotationSpeed { get; set; }
        public float SpeedUp { get; set; }
        public float SpeedUpDistance { get; set; }
        public float MinSpeed { get; set; }

        public void Validate()
        {
            if (Speed == 0)
                throw new Exception($"{nameof(Speed)} is empty");

            if (UnitSize == 0)
                throw new Exception($"{nameof(UnitSize)} is empty");

            if (Hairs == null || Hairs.Length == 0)
                throw new Exception($"{nameof(Hairs)} is empty");

            if (RotationSpeed == 0)
                throw new Exception($"{nameof(RotationSpeed)} is empty");

            if (SpeedUp == 0)
                throw new Exception($"{nameof(SpeedUp)} is empty");

            if (SpeedUpDistance == 0)
                throw new Exception($"{nameof(SpeedUpDistance)} is empty");

            if (MinSpeed == 0)
                throw new Exception($"{nameof(MinSpeed)} is empty");
        }
    }
}

