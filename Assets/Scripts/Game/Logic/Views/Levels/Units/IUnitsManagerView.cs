using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public interface IUnitsManagerView : IViewWithPresenter, IViewWithDefaultPresenter
    {
        IViewContainer Container { get; }
        IViewPrefab UnitPrototype { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new UnitsPresenter(this);
        }

    }
}