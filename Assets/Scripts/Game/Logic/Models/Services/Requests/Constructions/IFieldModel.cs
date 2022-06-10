using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Requests.Constructions
{
    public interface IFieldModel : IDisposable
    {
        Dictionary<IntPoint, CellPlacementStatus> Status { get; }
        Dictionary<IntPoint, GameVector3> Positions { get; } 
        FieldBoundaries Boudaries { get; }
    }
}
