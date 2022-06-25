using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Fields;

namespace Game.Assets.Scripts.Game.Logic.DataObjects.Fields
{
    public class FieldData : IData
    {
        public GroupOfPositions AvailableCells { get; set; }
        public FieldBoundaries Boundaries { get; set; }
        public IReadOnlyCollection<FieldPosition> Cells { get; set; }
    }
}