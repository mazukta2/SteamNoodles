using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class MainScreenUnityView : ScreenUnityView<MainScreenPresenter>, IMainScreenView
    {
        [SerializeField] HandUnityView _hand;
        [SerializeField] TextMeshProUGUI _points;
        [SerializeField] Image _progress;
        [SerializeField] Image _additionalProgress;
        [SerializeField] Image _negativeProgress;
        [SerializeField] ButtonUnityView _nextWave;
        [SerializeField] UnityProgressBar _nextWaveProgress;

        public IHandView HandView => _hand;
        public IText Points { get; private set; }
        public IPointsProgressBar PointsProgress { get; private set; }
        public IButton NextWaveButton => _nextWave;
        public IProgressBar NextWaveProgress => _nextWaveProgress;

        protected override void PreAwake()
        {
            Points = new UnityText(_points);

            PointsProgress = new UnityPointProgressBar(_progress, _additionalProgress, _negativeProgress);
        }
    }
}
