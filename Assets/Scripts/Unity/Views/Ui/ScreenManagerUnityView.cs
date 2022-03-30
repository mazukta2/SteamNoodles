using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ScreenManagerUnityView : UnityView<ScreenManagerView>
    {
        public ContainerUnityView Screen;

        protected override ScreenManagerView CreateView()
        {
            return new ScreenManagerView(Level, Screen);
        }

    }
}
