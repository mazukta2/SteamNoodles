namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources
{
    public record BuildingPoints
    {
        public BuildingPoints(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}
