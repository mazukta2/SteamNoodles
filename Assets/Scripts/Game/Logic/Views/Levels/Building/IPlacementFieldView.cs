using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Views.Assets;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IPlacementFieldView : IViewWithPresenter, IViewWithDefaultPresenter
    {
        IViewContainer ConstrcutionContainer { get; }
        IViewPrefab ConstrcutionPrototype { get; }
        IViewContainer CellsContainer { get; }
        IViewPrefab Cell { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new PlacementFieldPresenter(IGhostManagerView.Default.Presenter,
                IGameLevelPresenterRepository.Default.Constructions,
                IBattleLevel.Default.Field,
                IBattleLevel.Default.Building,
                this,
                IGameDefinitions.Default.Get<ConstructionsSettingsDefinition>(), IGameAssets.Default);
        }
    }
}