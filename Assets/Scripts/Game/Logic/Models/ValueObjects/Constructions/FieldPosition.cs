using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions
{
    // position RELATIVE to a field. it can be outside the field
    public record FieldPosition : CellPosition
    {
        private readonly Field _field;

        public FieldPosition(Field field, IntPoint value) : this(field, value.X, value.Y)
        {
        }

        public FieldPosition(Field field, int x, int y) : base(x, y)
        {
            _field = field ?? throw new ArgumentNullException(nameof(field));
        }

        public GameVector3 WorldPosition => _field.GetWorldPosition(this, new IntRect(0, 0, 1, 1));
    }
}
