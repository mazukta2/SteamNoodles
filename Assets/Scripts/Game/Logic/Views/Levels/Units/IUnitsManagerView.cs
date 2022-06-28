using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Units
{
    public interface IUnitsManagerView : IViewWithPresenter, IViewWithDefaultPresenter
    {
        IViewContainer Container { get; }
        IViewPrefab UnitPrototype { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new UnitsPresenter(IBattleLevel.Default.Units, this, IGameDefinitions.Default.Get<UnitsSettingsDefinition>());
        }

    }
}