using Assets.Scripts.Tests.Views.Ui.Screens.Elements;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Assets.Scripts.Tests.Views.Ui;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens
{
    public class BuildScreenView : ScreenView<BuildScreenPresenter>, IBuildScreenView
    {
        public IButton CancelButton { get; }
        public IWorldText Points { get; }
        public IText CurrentPoints { get; }
        public IPointsProgressBar PointsProgress { get; set; }

        public IViewContainer AdjacencyContainer { get; }
        public IViewPrefab AdjacencyPrefab { get; }

        public BuildScreenView(ILevelView level, IButton cancelButton, IWorldText points, IText currentPoints, IPointsProgressBar progressBar) : base(level)
        {
            CancelButton = cancelButton;
            Points = points;
            PointsProgress = progressBar;
            CurrentPoints = currentPoints;
            AdjacencyContainer = new ContainerViewMock(level);
            AdjacencyPrefab = new AdjecencyPrefab();

        }

        public class AdjecencyPrefab : ViewPrefabMock
        {
            public override IView CreateView<T>(ILevelView level, ContainerViewMock container)
            {
                return new AdjecencyTextView(level);
            }
        }
    }
}
