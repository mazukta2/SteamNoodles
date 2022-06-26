using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.DataObjects.Constructions
{
    public class ConstructionData : IData
    {
        public GameVector3 WorldPosition { get; set; }
        public FieldRotation Rotation { get; set; }
        public ConstructionScheme Scheme { get; set; }
    }
}