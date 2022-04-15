using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public class UnitsManagerView : PresenterView<UnitsPresenter>
    {
        public IViewContainer Container { get; private set; }
        public IViewPrefab UnitPrototype { get; private set; }

        public UnitsManagerView(ILevel level, IViewContainer container, IViewPrefab prototype) : base(level)
        {
            Container = container;
            UnitPrototype = prototype;

            new UnitsPresenter(Level.Model.Units, this, level.Engine.Definitions.Get<UnitsSettingsDefinition>());
        }
    }
}