using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Fields;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Fields
{
    public class FieldPresentation : Disposable, IAggregation
    {

        public Uid Id { get; }
        
        public GroupOfPositions AvailableCells { get; set; }
        public FieldBoundaries Boundaries { get; set; }
        public GroupOfPositions AllCells { get; set; }

        public static FieldPresentation Default()
        {
            var data = new FieldPresentation();
            data.AvailableCells = new GroupOfPositions(new List<FieldPosition>());
            data.Boundaries = new FieldBoundaries(new IntPoint(1,1));
            data.AllCells = new GroupOfPositions(new List<FieldPosition>());
            return data;
        }
    }
}