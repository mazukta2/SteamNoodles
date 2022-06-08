using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class FieldService : IService
    {
        private float _cellSize;
        private IntPoint _mapSize;

        public FieldService(float cellSize, IntPoint mapSize)
        {
            _cellSize = cellSize;
            _mapSize = mapSize;
        }

        public GameVector3 GetWorldPosition(Construction construction)
        {
            return GetMapPositionByGridPosition(construction.Position.Value, construction.GetSize());
        }

        public FieldPosition GetWorldConstructionToField(GameVector3 world, IntRect size)
        {
            return new FieldPosition(GetGridPositionByMapPosition(world, size));
        }

        public FieldBoundaries GetBoundaries()
        {
            return new FieldBoundaries(_mapSize);
        }

        public GameVector3 GetWorldPosition(FieldPosition position, IntRect sise)
        {
            return GetMapPositionByGridPosition(position.Value, sise);
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
