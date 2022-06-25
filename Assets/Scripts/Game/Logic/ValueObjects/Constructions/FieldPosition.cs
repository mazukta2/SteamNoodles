using System;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions
{
    // position RELATIVE to a field. it can be outside the field
    public record FieldPosition : CellPosition
    {
        public Field Field { get; private set; }

        public FieldPosition(Field field) : this(field, 0, 0)
        {
            
        }
        
        public FieldPosition(Field field, IntPoint value) : this(field, value.X, value.Y)
        {
        }

        public FieldPosition(Field field, int x, int y) : base(x, y)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
        }

        public GameVector3 GetWorldPosition()
        {
            return GetWorldPosition(new IntRect(0, 0, 1, 1));
        }
        
        public GameVector3 GetWorldPosition(IntRect size)
        {
            var position = Field.GetWorldPosition(Value);
            var offset = Field.GetOffset(size);
            return position + offset;
        }

        public bool IsInside()
        {
            return Field.GetBoundaries().IsInside(this);
        }
        
        public static FieldPosition operator +(FieldPosition current, CellPosition other) => new FieldPosition(current.Field,current.Value + other.Value);
        public static FieldPosition operator -(FieldPosition current, CellPosition other) => new FieldPosition(current.Field,current.Value - other.Value);
    }
}
