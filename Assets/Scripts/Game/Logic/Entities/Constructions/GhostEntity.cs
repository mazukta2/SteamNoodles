using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Fields;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Entities.Constructions
{
    public record GhostEntity : Entity
    {
        public event Action<GameVector3, GameVector3> OnMoved = delegate { };
        public event Action OnRotated = delegate { };
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
            var odl = TargetPosition;
            TargetPosition = pointerPosition;
            OnMoved(odl, pointerPosition);
        }

        public void SetRotation(FieldRotation rotation)
        {
            Rotation = rotation;
            OnRotated();
        }
    }
}
