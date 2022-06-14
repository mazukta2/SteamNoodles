using Game.Assets.Scripts.Game.Logic.Common.Helpers;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources
{
    public record BuildingPoints
    {
        private readonly int _value;
        
        public BuildingPoints(int value)
        {
            _value = value;
        }

        public static BuildingPoints Zero => new BuildingPoints(0);
        public int AsInt() => _value;
        public string AsString() => _value.GetSignedNumber();

        public static BuildingPoints operator +(BuildingPoints a, BuildingPoints b)
            => new BuildingPoints(a._value + b._value);
        
        public static BuildingPoints operator -(BuildingPoints a, BuildingPoints b)
            => new BuildingPoints(a._value - b._value);

        public string AsStringWithoutSing() => _value.ToString();
    }
}
