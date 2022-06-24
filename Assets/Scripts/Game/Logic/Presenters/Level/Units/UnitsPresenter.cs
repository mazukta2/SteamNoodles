using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Units;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitsPresenter : BasePresenter<IUnitsManagerView>
    {
        private IUnitsManagerView _unitsManagerView;
        private IQuery<Unit> _repository;
        private readonly IGameTime _time;

        public UnitsPresenter(IUnitsManagerView unitsManagerView) : this(unitsManagerView,
            IPresenterServices.Default?.Get<IQuery<Unit>>(),
            IGameTime.Default)
        {

        }

        public UnitsPresenter(IUnitsManagerView unitsManagerView, IQuery<Unit> units, IGameTime time) : base(unitsManagerView)
        {
            _unitsManagerView = unitsManagerView ?? throw new System.ArgumentNullException(nameof(unitsManagerView));
            _repository = units ?? throw new System.ArgumentNullException(nameof(units));
            _time = time ?? throw new System.ArgumentNullException(nameof(time));

            foreach (var item in _repository.Get())
                HandleOnAdded(item);

            _repository.OnAdded += HandleOnAdded;
        }

        protected override void DisposeInner()
        {
            _repository.Dispose();
            _repository.OnAdded -= HandleOnAdded;
        }

        private void HandleOnAdded(Unit unit)
        {
            var view = _unitsManagerView.Container.Spawn<IUnitView>(_unitsManagerView.UnitPrototype);
            new UnitPresenter(view, unit, _repository, _time);
        }

    }
}
