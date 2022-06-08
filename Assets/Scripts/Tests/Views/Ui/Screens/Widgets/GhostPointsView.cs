using Assets.Scripts.Tests.Views.Ui.Screens.Elements;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens
{
    public class GhostPointsView : ViewWithPresenter<GhostPointPresenter>, IGhostPointsView
    {
        public IWorldText Points { get; } = new UiWorldText();

        public IViewContainer AdjacencyContainer { get; }
        public IViewPrefab AdjacencyPrefab { get; }

        public GhostPointsView(IViewsCollection level) : base(level)
        {
            AdjacencyContainer = new ContainerViewMock(level);
            AdjacencyPrefab = new AdjecencyPrefab();

        }

        public class AdjecencyPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViewsCollection collection)
            {
                new AdjecencyTextView(collection);
            }
        }
    }
}
