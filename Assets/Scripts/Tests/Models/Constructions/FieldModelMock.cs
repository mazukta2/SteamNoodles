using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Models.Constructions
{
    public class FieldModelMock : Disposable, IFieldModel
    {
        public FieldModelMock(Dictionary<IntPoint, CellPlacementStatus> list,
            Dictionary<IntPoint, GameVector3> positions, FieldBoundaries boundaries)
        {
            Status = list;
            Positions = positions;
            Boudaries = boundaries;
        }

        public Dictionary<IntPoint, CellPlacementStatus> Status { get; private set; }
        public Dictionary<IntPoint, GameVector3> Positions { get; private set; }
        public FieldBoundaries Boudaries { get; private set; }

        public GameVector3 GetCellWorldPosition(IntPoint position)
        {
            return Positions[position];
        }
    }
}
