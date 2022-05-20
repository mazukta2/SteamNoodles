using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public enum CellPlacementStatus
    {
        Normal,
        IsReadyToPlace,
        IsAvailableGhostPlace,
        IsNotAvailableGhostPlace,
        IsUnderConstruction
    }
}
