using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Fields
{
    public record GroupOfPositions
    {
        public IReadOnlyCollection<FieldPosition> Cells { get; private set; }
        
        public GroupOfPositions(IReadOnlyCollection<FieldPosition> cells)
        {
            Cells = cells;
        }

        public GroupOfPositions(Field field)
        {
            var boundaries = field.GetBoundaries();
            var list = new List<FieldPosition>();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    list.Add(new FieldPosition(field, x, y));
                }
            }

            Cells = list;
        }
    }
}