using System;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Events.Fields;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Fields;

namespace Game.Assets.Scripts.Game.Logic.Entities.Constructions
{
    public record Field : Entity
    {

        private float _cellSize;
        private FieldBoundaries _boundaries;

        public Field() : this(1, new IntPoint(11, 11))
        {
        }
        
        public Field(float cellSize, IntPoint mapSize)
        {
            if (cellSize <= 0) throw new Exception("Field size can't be less or equal 0");
            if (mapSize.X <= 0) throw new Exception("Field size can't be less or equal 0");
            if (mapSize.Y <= 0) throw new Exception("Field size can't be less or equal 0");
            
            _cellSize = cellSize;
            _boundaries = new FieldBoundaries(mapSize);
        }

        public FieldBoundaries GetBoundaries()
        {
            return _boundaries;
        }

        public GameVector3 GetWorldPosition(IntPoint position)
        {
            return new GameVector3(position.X * _cellSize, 0, position.Y * _cellSize);
        }
        
        // any position to cell position
        public FieldPosition GetFieldPosition(GameVector3 position, IntRect objectSize)
        {
            var offset = GetOffset(objectSize);

            var pos = position - offset;
            var mousePosX = Math.Round(pos.X / _cellSize);
            var mousePosY = Math.Round(pos.Z / _cellSize);
            return new FieldPosition(this, (int)Math.Ceiling(mousePosX), (int)Math.Ceiling(mousePosY));
        }

        // any position to position on map
        public GameVector3 GetAlignWithAGrid(GameVector3 position, IntRect objectSize)
        {
            var worldCell = GetFieldPosition(position, objectSize);
            return worldCell.GetWorldPosition(objectSize);
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
