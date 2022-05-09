using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IPlacementFieldView : IViewWithPresenter, IViewWithDefaultPresenter
    {
        IViewContainer ConstrcutionContainer { get; }
        IViewPrefab ConstrcutionPrototype { get; }
        IViewContainer CellsContainer { get; }
        IViewPrefab Cell { get; }

        void IViewWithDefaultPresenter.Init()
        {
            new PlacementFieldPresenter(IGhostManagerPresenter.Default, ICurrentLevel.Default.Constructions, this,
                IDefinitions.Default.Get<ConstructionsSettingsDefinition>(), IAssets.Default);
        }
    }
}