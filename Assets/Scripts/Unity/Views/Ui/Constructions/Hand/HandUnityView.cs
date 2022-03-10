using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandUnityView : UnityView<HandView>
    {
        public ContainerUnityView Cards;
        public PrototypeUnityView CardPrototype;

        private HandView _viewPresenter;
        public override HandView GetView() => _viewPresenter;

        protected override void CreatedInner()
        {
            Cards.ForceAwake();
            CardPrototype.ForceAwake();
            _viewPresenter = new HandView(Level, Cards.ViewPresenter, CardPrototype.ViewPresenter);
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }

}
