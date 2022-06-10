using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public class CellModel : ICellModel
    {
        public CellModel(CellPlacementStatus status, GameVector3 worldPosition)
        {
            Status = status;
            WorldPosition = worldPosition;
        }

        public GameVector3 WorldPosition { get; internal set; }
        public CellPlacementStatus Status { get; internal set; }
    }
}
