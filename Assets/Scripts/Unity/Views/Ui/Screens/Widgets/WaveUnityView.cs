using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Screens.Widgets
{
    public class WaveUnityView : UnityView<WaveWidgetPresenter>, IWaveWidgetView
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
