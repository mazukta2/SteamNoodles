using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Entities.Constructions
{
    public record ConstructionEntity : Entity
    {
        public ConstructionSchemeEntity SchemeEntity { get; }
        public FieldPosition Position { get; }
        public FieldRotation Rotation { get; }

        public ConstructionEntity()
        {
            SchemeEntity = new ConstructionSchemeEntity();
            Position = new FieldPosition(new FieldEntity(),0, 0);
            Rotation = FieldRotation.Default;
        }
        
        public ConstructionEntity(FieldEntity fieldEntity)
        {
            SchemeEntity = new ConstructionSchemeEntity();
            Position = new FieldPosition(fieldEntity,0, 0);
            Rotation = FieldRotation.Default;
        }
        
        public ConstructionEntity(ConstructionSchemeEntity schemeEntity, FieldPosition position, FieldRotation rotation)
        {
            SchemeEntity = schemeEntity;
            Position = position;
            Rotation = rotation;
        }

        public IReadOnlyCollection<FieldPosition> GetOccupiedSpace()
        {
            return SchemeEntity.Placement.GetOccupiedSpace(Position, Rotation)
                .Select(x => x.AsFieldPosition(Position.FieldEntity)).AsReadOnly();
        }

        public IntRect GetSize()
        {
            return SchemeEntity.Placement.GetRect(Rotation);
        }

        public GameVector3 GetWorldPosition()
        {
            return Position.GetWorldPosition(GetSize());
        }
    }
}
