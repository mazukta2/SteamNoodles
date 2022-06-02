using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
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
            new PlacementFieldPresenter(this);
        }
    }
}