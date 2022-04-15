using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Tests.Environment.Views.Ui.Screens
{
    public class MainScreenView : ScreenView<MainScreenPresenter>, IMainScreenView
    {
        public IHandView HandView { get; set; }
        public IText Points { get; set; }
        public IProgressBar PointsProgress { get; set; }

        public MainScreenView(LevelView level, HandView handView, IText points, IProgressBar pointsProgress) : base(level)
        {
            HandView = handView;
            Points = points;
            PointsProgress = pointsProgress;
        }
    }
}
