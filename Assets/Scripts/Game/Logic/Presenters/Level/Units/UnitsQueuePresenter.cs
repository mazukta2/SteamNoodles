using Game.Assets.Scripts.Game.Logic.Views.Level.Units;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitsQueuePresenter : BasePresenter<IUnitsQueueView>
    {
        private IUnitsQueueView _unitsManagerView;

        public UnitsQueuePresenter(IUnitsQueueView view) : base(view)
        {
            _unitsManagerView = view;
        }

        protected override void DisposeInner()
        {
        }
    }
}
