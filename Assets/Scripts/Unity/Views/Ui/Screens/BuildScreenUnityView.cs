using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class BuildScreenUnityView : ScreenUnityView<BuildScreenView>
    {
        private BuildScreenView _viewPresenter;
        public override BuildScreenView GetView() => _viewPresenter;
        protected override void CreatedInner()
        {
            _viewPresenter = new BuildScreenView(Level);
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }

}
