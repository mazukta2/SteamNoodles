using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class BuildScreenUnityView : ScreenUnityView<BuildScreenView>
    {
        protected override BuildScreenView CreateView()
        {
            return new BuildScreenView(Level);
        }
    }

}
