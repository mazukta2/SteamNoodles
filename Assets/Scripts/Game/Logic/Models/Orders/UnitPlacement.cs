using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class UnitPlacement : Disposable
    {
        private Placement _placement;
        private readonly Dictionary<ServingCustomerProcess, Construction> _tables = new Dictionary<ServingCustomerProcess, Construction>();

        public UnitPlacement(Placement placement)
        {
            _placement = placement;
        }

        protected override void DisposeInner()
        {
        }

        public IReadOnlyCollection<Construction> GetFreePlacesToEat()
        {
            return _placement.GetConstructionsWithFeature<IPlaceToEatConstructionFeatureSettings>().Where(x => !IsAnybodyPlacedTo(x)).AsReadOnly();
        }

        public Construction GetOrderingPlace()
        {
            return _placement.GetConstructionsWithFeature<IOrderingPlaceConstructionFeatureSettings>().FirstOrDefault();
        }

        public void ClearPlacing(ServingCustomerProcess servingCustomerProcess)
        {
            _tables.Remove(servingCustomerProcess);
        }

        public void PlaceToTable(ServingCustomerProcess servingCustomerProcess, Construction construction)
        {
            _tables.Add(servingCustomerProcess, construction);
        }

        public void PlaceToOrderingTable(ServingCustomerProcess servingCustomerProcess)
        {
            if (IsAnybodyPlacedTo(GetOrderingPlace()))
                throw new Exception("Current order is not null");
            PlaceToTable(servingCustomerProcess, GetOrderingPlace());
        }

        public bool IsAnybodyPlacedTo(Construction construction)
        {
            return _tables.Values.Contains(construction);
        }

        public FloatPoint GetServingPoint()
        {
            return new FloatPoint(0, _placement.RealRect.yMin - _placement.CellSize * 1.5f);
        }

        public FloatPoint GetAwayPoint()
        {
            return new FloatPoint(0, _placement.RealRect.yMin - _placement.CellSize * 4f);
        }
    }
}
