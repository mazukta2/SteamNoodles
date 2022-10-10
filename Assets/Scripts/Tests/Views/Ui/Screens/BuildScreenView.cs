using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Elements;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens
{
    public class BuildScreenView : ScreenView<BuildScreenPresenter>, IBuildScreenView
    {
        public IWorldText Points { get; }

        public IViewContainer AdjacencyContainer { get; }
        public IViewPrefab AdjacencyPrefab { get; }

        public BuildScreenView(IViews level, IWorldText points) : base(level)
        {
            Points = points;
            AdjacencyContainer = new ContainerViewMock(level);
            AdjacencyPrefab = new AdjecencyPrefab();

        }

        public class AdjecencyPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViews collection)
            {
                new AdjecencyTextView(collection);
            }
        }
    }
}
