using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets
{
    public interface IEndWaveButtonView : IViewWithGenericPresenter<EndWaveButtonWidgetPresenter>, IViewWithDefaultPresenter
    {
        IButton NextWaveButton { get; }
        IButton FailWaveButton { get; }
        IProgressBar NextWaveProgress { get; }
        IAnimator WaveButtonAnimator { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new EndWaveButtonWidgetPresenter(this);
        }
    }
}
