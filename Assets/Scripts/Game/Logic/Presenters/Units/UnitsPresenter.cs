using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Units
{
    public class UnitsPresenter : Disposable
    {
        private LevelUnits _model;
        private IUnitsView _view;
        private List<UnitPresenter> _units = new List<UnitPresenter>();

        public UnitsPresenter(LevelUnits model, IUnitsView view)
        {
            _model = model;
            _view = view;

            foreach (var unit in _model.Units)
            {
                SpawnUnit(unit);
            }
            _model.OnUnitSpawn += SpawnUnit;
            _model.OnUnitDestroy += DestroyUnit;
        }

        protected override void DisposeInner()
        {
            _model.OnUnitSpawn -= SpawnUnit;
            foreach (var item in _units)
                item.Dispose();

            _units.Clear();
        }

        private void SpawnUnit(Unit unit)
        {
            var vm = new UnitPresenter(unit, _view.CreateUnit());
            _units.Add(vm);
        }

        private void DestroyUnit(Unit obj)
        {
            foreach (var item in _units.ToList())
            {
                if (item.Unit == obj)
                {
                    _units.Remove(item);
                    item.Dispose();
                }
            }
        }
    }
}
