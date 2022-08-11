using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public interface IWaveWidgetView : IViewWithGenericPresenter<WaveWidgetPresenter>, IViewWithDefaultPresenter
    {
        IButton NextWaveButton { get; }
        IButton FailWaveButton { get; }
        IProgressBar NextWaveProgress { get; }
        IAnimator WaveButtonAnimator { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new WaveWidgetPresenter(this);
        }
    }
}
