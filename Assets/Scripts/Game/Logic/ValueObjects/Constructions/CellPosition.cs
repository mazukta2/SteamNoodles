﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions
{
    public record CellPosition
    {
        public int X => Value.X;
        public int Y => Value.Y;

        public IntPoint Value { get; }

        public CellPosition(IntPoint value) : this(value.X, value.Y)
        {
            
        }

        public CellPosition(int x, int y)
        {
            Value = new IntPoint(x, y);
        }
        
        public FieldPosition AsFieldPosition(FieldEntity fieldEntity)
        {
            return new FieldPosition(fieldEntity, X, Y);
        }

        public static CellPosition operator +(CellPosition current, CellPosition other) => new CellPosition(current.Value + other.Value);
        public static CellPosition operator -(CellPosition current, CellPosition other) => new CellPosition(current.Value - other.Value);

    }
}
