using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class FieldPositionsCalculator
    {
        private float _cellSize;
        private IntRect _size;

        public FieldPositionsCalculator(float cellSize, IntRect size)
        {
            _cellSize = cellSize;
            _size = size;
        }

        public IntPoint GetWorldCellPosition(FloatPoint pointer)
        {
            var halfCell = _cellSize / 2;

            var offset = new FloatPoint(_size.Width * halfCell - halfCell, _size.Height * halfCell - halfCell);

            var pos = pointer - offset;
            var mousePosX = Math.Round(pos.X / _cellSize);
            var mousePosY = Math.Round(pos.Y / _cellSize);
            return new IntPoint((int)Math.Ceiling(mousePosX), (int)Math.Ceiling(mousePosY));
        }

        public FloatPoint GetViewPosition(FloatPoint pointer)
        {
            var worldCell = GetWorldCellPosition(pointer);

            return GetViewPositionByWorldPosition(worldCell);
            //var offset = GetViewOffsetPosition();
            //return new FloatPoint(worldCell.X * _cellSize, worldCell.Y * _cellSize) + offset;
        }

        public FloatPoint GetViewPositionByWorldPosition(IntPoint worldCell)
        {
            var offset = GetViewOffsetPosition();
            return new FloatPoint(worldCell.X * _cellSize, worldCell.Y * _cellSize) + offset;
        }

        private FloatPoint GetViewOffsetPosition()
        {
            var halfCell = _cellSize / 2;
            var offset = new FloatPoint(_size.Width * halfCell - halfCell, _size.Height * halfCell - halfCell);
            return offset;
        }
    }
}
