using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
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
            new UnitsPresenter(IGameLevelPresenterRepository.Default.Units, this, IGameDefinitions.Default.Get<UnitsSettingsDefinition>());
        }

    }
}