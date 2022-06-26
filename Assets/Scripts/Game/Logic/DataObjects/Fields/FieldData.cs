using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Fields;

namespace Game.Assets.Scripts.Game.Logic.DataObjects.Fields
{
    public class FieldData : IData
    {
        public GroupOfPositions AvailableCells { get; set; }
        public FieldBoundaries Boundaries { get; set; }
        public GroupOfPositions AllCells { get; set; }

        public FieldData()
        {
            AvailableCells = new GroupOfPositions(new List<FieldPosition>());
            Boundaries = new FieldBoundaries(new IntPoint(1,1));
            AllCells = new GroupOfPositions(new List<FieldPosition>());
            
        }
    }
}