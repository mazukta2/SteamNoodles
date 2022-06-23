using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions
{
    public record ConstructionGhost : Entity
    {
        public ConstructionCard Card { get; }
        public CellPosition Position { get; private set; }
        public FieldRotation Rotation { get; private set;}
        public GameVector3 TargetPosition { get; private set;}

        public ConstructionGhost(ConstructionCard card, CellPosition position, GameVector3 targetPosition, FieldRotation rotation)
        {
            Card = card;
            Position = position;
            Rotation = rotation;
            TargetPosition = targetPosition;
        }

        public void SetPosition(CellPosition cellPosition, GameVector3 pointerPosition)
        {
            Position = cellPosition;
            TargetPosition = pointerPosition;
        }

        public void SetRotation(FieldRotation rotation)
        {
            Rotation = rotation;
        }
    }
}
