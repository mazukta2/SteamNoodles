using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ScreenManagerUnityView : UnityView<ScreenManagerView>
    {
        public ContainerUnityView Screen;

        private ScreenManagerView _viewPresenter;
        public override ScreenManagerView GetView() => _viewPresenter;

        protected override void CreatedInner()
        {
            Screen.ForceAwake();
            _viewPresenter = new ScreenManagerView(Level, Screen.ViewPresenter);
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }
}
