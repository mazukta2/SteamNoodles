using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Requests.Constructions
{
    public class GetField : Request
    {
        public State Respond { get; set; } = new();

        public class State
        {
            public Dictionary<IntPoint, CellPlacementStatus> Status { get; private set; } = new();
            public Dictionary<IntPoint, GameVector3> Positions { get; private set; } = new();
            public FieldBoundaries Boudaries { get; private set; } = new(new IntPoint(1,1));

            public void SetCells(Dictionary<IntPoint, CellPlacementStatus> list,
                Dictionary<IntPoint, GameVector3> positions,
                FieldBoundaries boundaries)
            {
                Status = list;
                Positions = positions;
                Boudaries = boundaries;
            }

            public GameVector3 GetCellWorldPosition(IntPoint position)
            {
                return Positions[position];
            }
        }
    }
}
