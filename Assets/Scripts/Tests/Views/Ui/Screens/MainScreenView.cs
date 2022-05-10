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
        public IButton NextWaveButton { get; set; } = new ButtonMock();
        public IButton FailWaveButton { get; set; } = new ButtonMock();
        public IProgressBar NextWaveProgress { get; set; } = new ProgressBar();
        public AnimatorMock WaveButtonAnimator { get; set; } = new AnimatorMock();
        IAnimator IMainScreenView.WaveButtonAnimator => WaveButtonAnimator;

        public MainScreenView(ILevelView level, HandView handView) : base(level)
        {
            HandView = handView;
        }
    }
}
