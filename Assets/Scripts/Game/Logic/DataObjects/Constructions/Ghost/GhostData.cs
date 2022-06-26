using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.DataObjects.Constructions.Ghost
{
    public class GhostData : IData
    {
        public bool CanBuild { get; set; }
        public BuildingPoints Points { get; set; }
        public ConstructionCard Card { get; set; }
        public FieldPosition Position { get; set; }
        public FieldRotation Rotation { get; set; }
        public GameVector3 TargetPosition { get; set; }
        public IDataProvider<ConstructionCardData> CardData { get; set; }
    }
}