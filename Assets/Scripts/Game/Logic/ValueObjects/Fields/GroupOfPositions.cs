using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Fields
{
    public record GroupOfPositions
    {
        public IReadOnlyCollection<FieldPosition> Cells { get; private set; }
        
        public GroupOfPositions(IReadOnlyCollection<FieldPosition> cells)
        {
            Cells = cells;
        }

        public GroupOfPositions(FieldEntity fieldEntity)
        {
            var boundaries = fieldEntity.GetBoundaries();
            var list = new List<FieldPosition>();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    list.Add(new FieldPosition(fieldEntity, x, y));
                }
            }

            Cells = list;
        }
    }
}