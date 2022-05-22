using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class MainScreenUnityView : ScreenUnityView<MainScreenPresenter>, IMainScreenView
    {
        [SerializeField] ButtonUnityView _nextWave;
        [SerializeField] ButtonUnityView _failWave;
        [SerializeField] UnityProgressBar _nextWaveProgress;
        [SerializeField] AnimatorUnity _waveButtonAnimator;

        public IButton NextWaveButton => _nextWave;
        public IProgressBar NextWaveProgress => _nextWaveProgress;
        public IButton FailWaveButton => _failWave;
        public IAnimator WaveButtonAnimator => _waveButtonAnimator;
    }
}
