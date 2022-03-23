using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using TMPro;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class BuildScreenUnityView : ScreenUnityView<BuildScreenView>
    {
        [SerializeField] ButtonUnityView _closeButton;
        [SerializeField] TextMeshProUGUI _additionalPoints;
        protected override BuildScreenView CreateView()
        {
            return new BuildScreenView(Level, _closeButton.View, new UnityText(_additionalPoints));
        }
    }

}
