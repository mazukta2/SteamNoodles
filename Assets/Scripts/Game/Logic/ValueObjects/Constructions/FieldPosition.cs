using System;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions
{
    // position RELATIVE to a field. it can be outside the field
    public record FieldPosition : CellPosition
    {
        public FieldEntity FieldEntity { get; private set; }

        public FieldPosition(FieldEntity fieldEntity) : this(fieldEntity, 0, 0)
        {
            
        }
        
        public FieldPosition(FieldEntity fieldEntity, IntPoint value) : this(fieldEntity, value.X, value.Y)
        {
        }

        public FieldPosition(FieldEntity fieldEntity, int x, int y) : base(x, y)
        {
            FieldEntity = fieldEntity ?? throw new ArgumentNullException(nameof(fieldEntity));
        }

        public GameVector3 GetWorldPosition()
        {
            return GetWorldPosition(new IntRect(0, 0, 1, 1));
        }
        
        public GameVector3 GetWorldPosition(IntRect size)
        {
            var position = FieldEntity.GetWorldPosition(Value);
            var offset = FieldEntity.GetOffset(size);
            return position + offset;
        }

        public bool IsInside()
        {
            return FieldEntity.GetBoundaries().IsInside(this);
        }
        
        public static FieldPosition operator +(FieldPosition current, CellPosition other) => new FieldPosition(current.FieldEntity,current.Value + other.Value);
        public static FieldPosition operator -(FieldPosition current, CellPosition other) => new FieldPosition(current.FieldEntity,current.Value - other.Value);
    }
}
