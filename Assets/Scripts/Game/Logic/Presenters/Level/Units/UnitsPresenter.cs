using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitsPresenter : BasePresenter<IUnitsManagerView>
    {
        private IUnitsManagerView _unitsManagerView;
        private IPresenterRepository<Unit> _repository;
        private readonly IGameTime _time;

        public UnitsPresenter(IUnitsManagerView unitsManagerView) : this(unitsManagerView, 
            IStageLevelPresenterRepositories.Default.Units,
            IGameTime.Default)
        {

        }

        public UnitsPresenter(IUnitsManagerView unitsManagerView, IPresenterRepository<Unit> units, IGameTime time) : base(unitsManagerView)
        {
            _unitsManagerView = unitsManagerView ?? throw new System.ArgumentNullException(nameof(unitsManagerView));
            _repository = units ?? throw new System.ArgumentNullException(nameof(units));
            _time = time ?? throw new System.ArgumentNullException(nameof(time));

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
            new UnitPresenter(view, link, _time);
        }
    }
}
