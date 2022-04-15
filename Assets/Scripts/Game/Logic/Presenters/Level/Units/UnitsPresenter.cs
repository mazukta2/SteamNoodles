using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitsPresenter : BasePresenter<UnitsManagerView>
    {
        private UnitsManagerView _unitsManagerView;
        private LevelUnits _model;
        private UnitsSettingsDefinition _settingsDefinition;

        public UnitsPresenter(LevelUnits levelUnits, UnitsManagerView unitsManagerView, UnitsSettingsDefinition settingsDefinition) : base(unitsManagerView)
        {
            _unitsManagerView = unitsManagerView;
            _model = levelUnits;
            _settingsDefinition = settingsDefinition;

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
            new UnitPresenter(item, view, _settingsDefinition);
        }
    }
}
