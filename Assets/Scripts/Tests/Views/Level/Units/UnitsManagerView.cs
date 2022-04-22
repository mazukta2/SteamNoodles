using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;

namespace Game.Assets.Scripts.Tests.Views.Level.Units
{
    public class UnitsManagerView : PresenterView<UnitsPresenter>, IUnitsManagerView
    {
        public IViewContainer Container { get; private set; }
        public IViewPrefab UnitPrototype { get; private set; }

        public UnitsManagerView(LevelView level, IViewContainer container, IViewPrefab prototype) : base(level)
        {
            Container = container;
            UnitPrototype = prototype;
        }
    }
}