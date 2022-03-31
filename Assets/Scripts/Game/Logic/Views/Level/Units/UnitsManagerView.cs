using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public class UnitsManagerView : View
    {
        public IViewContainer Container { get; private set; }
        public IViewPrefab UnitPrototype { get; private set; }

        private UnitsPresenter _presenter;

        public UnitsManagerView(ILevel level, IViewContainer container, IViewPrefab prototype) : base(level)
        {
            Container = container;
            UnitPrototype = prototype;

            _presenter = new UnitsPresenter(Level.Model.Units, this);
        }
    }
}