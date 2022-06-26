using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Units;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Units;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitsPresenter : BasePresenter<IUnitsManagerView>
    {
        private IUnitsManagerView _unitsManagerView;
        private IDataCollectionProvider<UnitData> _units;
        private readonly IGameTime _time;

        public UnitsPresenter(IUnitsManagerView unitsManagerView) : this(unitsManagerView,
            IPresenterServices.Default?.Get<IDataCollectionProviderService<UnitData>>().Get(),
            IGameTime.Default)
        {

        }

        public UnitsPresenter(IUnitsManagerView unitsManagerView, IDataCollectionProvider<UnitData> units, IGameTime time) : base(unitsManagerView)
        {
            _unitsManagerView = unitsManagerView ?? throw new System.ArgumentNullException(nameof(unitsManagerView));
            _units = units ?? throw new System.ArgumentNullException(nameof(units));
            _time = time ?? throw new System.ArgumentNullException(nameof(time));

            foreach (var item in _units.Get())
                HandleOnAdded(item);

            _units.OnAdded += HandleOnAdded;
        }

        protected override void DisposeInner()
        {
            _units.OnAdded -= HandleOnAdded;
        }

        private void HandleOnAdded(IDataProvider<UnitData> unit)
        {
            var view = _unitsManagerView.Container.Spawn<IUnitView>(_unitsManagerView.UnitPrototype);
            new UnitPresenter(view, unit, _time);
        }

    }
}
