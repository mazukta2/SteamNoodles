using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens
{
    public class MainScreenView : ScreenView<MainScreenPresenter>, IMainScreenView
    {
        public IHandView HandView { get; set; }
        public IText Points { get; set; }
        public IPointsProgressBar PointsProgress { get; set; }
        public IButton NextWaveButton { get; set; } = new ButtonMock();
        public IProgressBar NextWaveProgress { get; set; } = new ProgressBar();

        public MainScreenView(LevelView level, HandView handView, IText points, IPointsProgressBar pointsProgress) : base(level)
        {
            HandView = handView;
            Points = points;
            PointsProgress = pointsProgress;
        }
    }
}
