namespace Game.Assets.Scripts.Game.Logic.Definitions.Constructions
{
    public class ConstructionsSettingsDefinition
    {
        public float CellSize { get; set; }
        public float LevelUpPower { get; set; }
        public float LevelUpOffset { get; set; }
        public float GhostShrinkDistance { get; set; }
        public float GhostHalfShrinkDistance { get; set; }

        public float PieceSpawningTime { get; set; }
        public float PieceMovingTime { get; set; }
        public float PointsSliderFrequency { get; set; }
        public float PointsSliderSpeed { get; set; }

        public void Validate()
        {

        }
    }
}

