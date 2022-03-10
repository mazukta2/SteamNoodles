using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionUnityView : UnityView<HandConstructionView>
    {
        public ButtonUnityView Button;

        private HandConstructionView _viewPresenter;
        public override HandConstructionView GetView() => _viewPresenter;

        protected override void CreatedInner()
        {
            Button.ForceAwake();
            _viewPresenter = new HandConstructionView(Level, Button.ViewPresenter);
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }

}
