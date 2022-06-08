using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens
{
    public class EndWaveButtonView : ViewWithPresenter<EndWaveButtonWidgetPresenter>, IEndWaveButtonView
    {
        public IButton NextWaveButton { get; set; } = new ButtonMock();
        public IButton FailWaveButton { get; set; } = new ButtonMock();
        public IProgressBar NextWaveProgress { get; set; } = new ProgressBar();
        public AnimatorMock WaveButtonAnimator { get; set; } = new AnimatorMock();
        IAnimator IEndWaveButtonView.WaveButtonAnimator => WaveButtonAnimator;

        public EndWaveButtonView(IViewsCollection level) : base(level)
        {

        }
    }
}
