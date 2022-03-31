using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitsPresenter : BasePresenter
    {
        private UnitsManagerView _unitsManagerView;
        private LevelUnits _model;

        public UnitsPresenter(LevelUnits levelUnits, UnitsManagerView unitsManagerView) : base(unitsManagerView)
        {
            _unitsManagerView = unitsManagerView;
            _model = levelUnits;

            foreach (var item in _model.Units)
            {
                SpawnUnit(item);
            }

            _model.OnUnitSpawn += SpawnUnit;
        }
        protected override void DisposeInner()
        {
            _model.OnUnitSpawn -= SpawnUnit;
        }

        private void SpawnUnit(Unit item)
        {
            var view = _unitsManagerView.Container.Spawn<UnitView>(_unitsManagerView.UnitPrototype);
            view.Init(item);
        }
    }
}
