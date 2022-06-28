using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Constructions
{
    public class ConstructionPresentation : Disposable, IAggregation
    {

        public Uid Id { get; }
        public GameVector3 WorldPosition { get; set; }
        public FieldRotation Rotation { get; set; }
        public ConstructionScheme Scheme { get; set; }

        public ConstructionPresentation(ConstructionScheme scheme)
        {
            Scheme = scheme;
            Rotation = FieldRotation.Default;
            WorldPosition = GameVector3.Zero;
        }
    }
}