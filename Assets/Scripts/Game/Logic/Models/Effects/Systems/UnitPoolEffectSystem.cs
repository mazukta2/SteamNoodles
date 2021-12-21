using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Effects.Systems
{
    public class UnitPoolEffectSystem : Disposable, IEffectSystem
    {
        private Placement _placement;
        private LevelUnits _units;

        public UnitPoolEffectSystem(Placement placement, LevelUnits units)
        {
            _placement = placement;
            _units = units;
            _placement.OnConstructionAdded += _placement_OnConstructionAdded;
            _placement.OnConstructionRemoved += _placement_OnConstructionRemoved;
        }

        protected override void DisposeInner()
        {
            _placement.OnConstructionAdded -= _placement_OnConstructionAdded;
            _placement.OnConstructionRemoved -= _placement_OnConstructionRemoved;
        }

        private void _placement_OnConstructionAdded(Construction obj)
        {
            foreach (var feature in obj.GetFeatures().OfType<INewCustomerConstructionFeatureSettings>())
            {
                _units.AddPotentialCustumer(feature.Customer);
            }
        }

        private void _placement_OnConstructionRemoved(Construction obj)
        {
            foreach (var feature in obj.GetFeatures().OfType<INewCustomerConstructionFeatureSettings>())
            {
                _units.RemovePotentialCustomer(feature.Customer);
            }
        }
    }
}
