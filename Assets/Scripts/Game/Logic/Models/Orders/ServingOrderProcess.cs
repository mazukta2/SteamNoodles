using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class ServingOrderProcess
    {
        public ServingOrderProcess(Placement placement, Unit unit)
        {
            _placement = placement;
            Unit = unit;
            IsOpen = true;
            Start();
        }

        public bool IsOpen { get; private set; }

        private Placement _placement;

        public Unit Unit { get; internal set; }

        public void Break()
        {
            IsOpen = false;
        }

        private void Start()
        {
            Unit.SetTarget(GetServingPoint());
        }

        private FloatPoint GetServingPoint()
        {
            return new FloatPoint(0, _placement.RealRect.yMin - _placement.CellSize * 1.5f);
        }
    }
}
