using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Units;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitsPresenter : BasePresenter<IUnitsManagerView>
    {
        private IUnitsManagerView _unitsManagerView;
        private IUnits _model;
        private UnitsSettingsDefinition _settingsDefinition;

        public UnitsPresenter(IUnits levelUnits, IUnitsManagerView unitsManagerView, UnitsSettingsDefinition settingsDefinition) : base(unitsManagerView)
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
            var view = _unitsManagerView.Container.Spawn<IUnitView>(_unitsManagerView.UnitPrototype);
            new UnitPresenter(item, view, _settingsDefinition, IGameTime.Default);
        }
    }
}
