using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions
{
    public record ConstructionGhost : Entity
    {
        public ConstructionCard Card { get; }
        public FieldPosition Position { get; private set; }
        public FieldRotation Rotation { get; private set;}
        public GameVector3 TargetPosition { get; private set;}

        public ConstructionGhost(ConstructionCard card, FieldPosition position, GameVector3 targetPosition, FieldRotation rotation)
        {
            Card = card;
            Position = position;
            Rotation = rotation;
            TargetPosition = targetPosition;
        }

        public void SetPosition(FieldPosition fieldPosition, GameVector3 pointerPosition)
        {
            Position = fieldPosition;
            TargetPosition = pointerPosition;
        }

        public void SetRotation(FieldRotation rotation)
        {
            Rotation = rotation;
        }
    }
}
