using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitsPresenter : BasePresenter
    {
        //private LevelUnits _model;
        //private IUnitsView _view;
        //private List<UnitPresenter> _units = new List<UnitPresenter>();

        private UnitsManagerView _unitsManagerView;

        public UnitsPresenter(UnitsManagerView unitsManagerView) : base(unitsManagerView)
        {
            _unitsManagerView = unitsManagerView;
        }
        //public UnitsPresenter(LevelUnits model, IUnitsView view)
        //{
        //    _model = model;
        //    _view = view;

        //    foreach (var unit in _model.Units)
        //    {
        //        SpawnUnit(unit);
        //    }
        //    _model.OnUnitSpawn += SpawnUnit;
        //    _model.OnUnitDestroy += DestroyUnit;
        //}

        //protected override void DisposeInner()
        //{
        //    _model.OnUnitSpawn -= SpawnUnit;
        //    foreach (var item in _units)
        //        item.Dispose();

        //    _units.Clear();
        //}

        //private void SpawnUnit(Unit unit)
        //{
        //    var vm = new UnitPresenter(unit, _view.CreateUnit());
        //    _units.Add(vm);
        //}

        //private void DestroyUnit(Unit obj)
        //{
        //    foreach (var item in _units.ToList())
        //    {
        //        if (item.Unit == obj)
        //        {
        //            _units.Remove(item);
        //            item.Dispose();
        //        }
        //    }
        //}
    }
}
