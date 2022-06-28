using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Fields;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Fields
{
    public class Field : Disposable, IAggregation
    {
        private readonly FieldEntity _fieldEntity;
        private readonly IDatabase<ConstructionEntity> _constructions;
        public Uid Id { get; }

        public Field(FieldEntity fieldEntity, IDatabase<ConstructionEntity> constructions)
        {
            _fieldEntity = fieldEntity;
            _constructions = constructions;
        }

        public GroupOfPositions GetFreeCells()
        {
            var list = new HashSet<FieldPosition>();
            var occupiedSpace = _constructions.Get()
                .SelectMany(otherBuilding => otherBuilding.GetOccupiedSpace()).AsReadOnly();
            
            var boundaries = _fieldEntity.GetBoundaries();
            var minY = boundaries.Value.Y;
            var maxY = boundaries.Value.Y + boundaries.Value.Height;
            
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    var fieldPosition = new FieldPosition(_fieldEntity, x, y);
                    
                    if (!(minY <= y && y < maxY))
                        continue;

                    if (occupiedSpace.Contains(fieldPosition))
                        continue;
                    
                    list.Add(fieldPosition);
                }
            }

            return new GroupOfPositions(list);
        }

        public FieldBoundaries GetBoundaries() => _fieldEntity.GetBoundaries();
        public GroupOfPositions GetCellsPositions() => _fieldEntity.GetAllCells();
    }
}