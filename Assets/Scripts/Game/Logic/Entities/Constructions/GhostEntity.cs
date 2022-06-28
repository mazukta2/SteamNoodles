using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts;
using Game.Assets.Scripts.Game.Logic.Aggregations.Fields;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Entities.Constructions
{
    public record GhostEntity : Entity
    {
        public ConstructionCardEntity CardEntity { get; }
        public FieldRotation Rotation { get; private set;}
        public GameVector3 TargetPosition { get; private set;}
        
        public GhostEntity(ConstructionCardEntity cardEntity) : this(cardEntity,GameVector3.Zero, FieldRotation.Default)
        {
            
        }
        
        public GhostEntity(ConstructionCardEntity cardEntity, GameVector3 targetPosition, FieldRotation rotation)
        {
            CardEntity = cardEntity;
            Rotation = rotation;
            TargetPosition = targetPosition;
        }

        public void SetPosition(GameVector3 pointerPosition)
        {
            TargetPosition = pointerPosition;
        }

        public void SetRotation(FieldRotation rotation)
        {
            Rotation = rotation;
            FireEvent(new GhostMovedEvent());
        }
    }
}
