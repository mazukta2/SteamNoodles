using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public interface IMainScreenView : IScreenView
    {
        IHandView HandView { get; }
        IButton NextWaveButton { get; }
        IButton FailWaveButton { get; }
        IProgressBar NextWaveProgress { get; }
        IAnimator WaveButtonAnimator { get; }
    }
}
