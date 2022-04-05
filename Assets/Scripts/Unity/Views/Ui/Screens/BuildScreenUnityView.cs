using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class BuildScreenUnityView : ScreenUnityView<BuildScreenView>
    {
        [SerializeField] ButtonUnityView _closeButton;
        [SerializeField] TextMeshProUGUI _additionalPoints;
        [SerializeField] TextMeshProUGUI _currentPoints;
        [SerializeField] Image _progress;
        [SerializeField] Image _additionalProgress;
        protected override BuildScreenView CreateView()
        {
            return new BuildScreenView(Level, _closeButton, 
                new UnityText(_additionalPoints), 
                new UnityText(_currentPoints),
                new UnityProgressBar(_progress, _additionalProgress));
        }
    }

}
