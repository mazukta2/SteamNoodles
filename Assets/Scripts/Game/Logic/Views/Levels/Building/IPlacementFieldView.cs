using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Assets;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Building
{
    public interface IPlacementFieldView : IViewWithPresenter, IViewWithDefaultPresenter
    {
        IViewContainer ConstrcutionContainer { get; }
        IViewPrefab ConstrcutionPrototype { get; }
        IViewContainer CellsContainer { get; }
        IViewPrefab Cell { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new PlacementFieldPresenter(IGhostManagerView.Default.Presenter, IModels.Default.Find<PlacementField>(), this,
                IDefinitions.Default.Get<ConstructionsSettingsDefinition>(), IGameAssets.Default);
        }
    }
}