﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class FieldPositionsCalculator
    {
        private float _cellSize;

        public FieldPositionsCalculator(float cellSize)
        {
            _cellSize = cellSize;
        }

        // any position to cell position
        public IntPoint GetGridPositionByMapPosition(GameVector3 position, IntRect objectSize)
        {
            var offset = GetOffset(objectSize);

            var pos = position - offset;
            var mousePosX = Math.Round(pos.X / _cellSize);
            var mousePosY = Math.Round(pos.Z / _cellSize);
            return new IntPoint((int)Math.Ceiling(mousePosX), (int)Math.Ceiling(mousePosY));
        }

        // cell position to position on map
        public GameVector3 GetMapPositionByGridPosition(IntPoint worldCell, IntRect objectSize)
        {
            var offset = GetOffset(objectSize);
            return new GameVector3(worldCell.X * _cellSize, 0, worldCell.Y * _cellSize) + offset;
        }

        // any position to position on map
        public GameVector3 GetAlignWithAGrid(GameVector3 position, IntRect objectSize)
        {
            var worldCell = GetGridPositionByMapPosition(position, objectSize);
            return GetMapPositionByGridPosition(worldCell, objectSize);
        }

        // object offset
        public GameVector3 GetOffset(IntRect objectSize)
        {
            var halfCell = _cellSize / 2;
            var offset = new GameVector3(objectSize.Width * halfCell - halfCell, 0, objectSize.Height * halfCell - halfCell);
            return offset;
        }
    }
}