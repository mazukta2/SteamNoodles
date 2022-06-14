using System;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
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

        public FieldBoundaries GetBoundaries()
        {
            return new FieldBoundaries(_mapSize);
        }

        public GameVector3 GetWorldPosition(Construction construction)
        {
            return GetWorldPosition(construction.Position, construction.GetSize());
        }

        public GameVector3 GetWorldPosition(FieldPosition position, IntRect size)
        {
            var offset = GetOffset(size);
            return new GameVector3(position.X * _cellSize, 0, position.Y * _cellSize) + offset;
        }

        // any position to cell position
        public FieldPosition GetFieldPosition(GameVector3 position, IntRect objectSize)
        {
            var offset = GetOffset(objectSize);

            var pos = position - offset;
            var mousePosX = Math.Round(pos.X / _cellSize);
            var mousePosY = Math.Round(pos.Z / _cellSize);
            return new FieldPosition((int)Math.Ceiling(mousePosX), (int)Math.Ceiling(mousePosY));
        }

        // any position to position on map
        public GameVector3 GetAlignWithAGrid(GameVector3 position, IntRect objectSize)
        {
            var worldCell = GetFieldPosition(position, objectSize);
            return GetWorldPosition(worldCell, objectSize);
        }

        // object offset
        private GameVector3 GetOffset(IntRect objectSize)
        {
            var halfCell = _cellSize / 2;
            var offset = new GameVector3(objectSize.Width * halfCell - halfCell, 0, objectSize.Height * halfCell - halfCell);
            return offset;
        }

    }
}
