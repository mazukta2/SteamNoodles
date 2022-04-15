using System;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Constructions
{
    public class UnitsSettingsDefinition
    {
        public float Speed { get; set; }
        public float SpeedOffset { get; set; }
        public float UnitSize { get; set; }
        public string[] Hairs { get; set; }

        public void Validate()
        {
            if (Speed == 0)
                throw new Exception($"{nameof(Speed)} is empty");

            if (UnitSize == 0)
                throw new Exception($"{nameof(UnitSize)} is empty");

            if (Hairs == null || Hairs.Length == 0)
                throw new Exception($"{nameof(Hairs)} is empty");
        }
    }
}

