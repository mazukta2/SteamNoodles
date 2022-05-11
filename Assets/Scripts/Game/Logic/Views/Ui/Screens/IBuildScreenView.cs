using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public interface IBuildScreenView : IScreenView, IViewWithGenericPresenter<BuildScreenPresenter>
    {
        IWorldText Points { get; }
        IViewContainer AdjacencyContainer { get; }
        IViewPrefab AdjacencyPrefab { get; }
    }
}
