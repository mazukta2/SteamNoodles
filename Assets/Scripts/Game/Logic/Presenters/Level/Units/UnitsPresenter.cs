using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitsPresenter : BasePresenter<IUnitsManagerView>
    {
        private IUnitsManagerView _unitsManagerView;
        private IPresenterRepository<Unit> _repository;
        private UnitsSettingsDefinition _settingsDefinition;

        public UnitsPresenter(IPresenterRepository<Unit> units, IUnitsManagerView unitsManagerView, UnitsSettingsDefinition settingsDefinition) : base(unitsManagerView)
        {
            _unitsManagerView = unitsManagerView;
            _repository = units;
            _settingsDefinition = settingsDefinition;

            foreach (var item in _repository.Get())
                SpawnUnit(item);

            _repository.OnAdded += HandleOnAdded;
        }

        protected override void DisposeInner()
        {
            _repository.OnAdded -= HandleOnAdded;
        }

        private void HandleOnAdded(EntityLink<Unit> link, Unit unit)
        {
            SpawnUnit(link);
        }

        private void SpawnUnit(EntityLink<Unit> link)
        {
            var view = _unitsManagerView.Container.Spawn<IUnitView>(_unitsManagerView.UnitPrototype);
            new UnitPresenter(link, view, _settingsDefinition, IGameTime.Default);
        }
    }
}
