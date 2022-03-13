using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class BuildScreenUnityView : ScreenUnityView<BuildScreenView>
    {
        [SerializeField] ButtonUnityView _closeButton;
        protected override BuildScreenView CreateView()
        {
            return new BuildScreenView(Level, _closeButton.View);
        }
    }

}
