using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandUnityView : UnityView<HandView>
    {
        public ContainerUnityView Cards;
        public PrototypeUnityView CardPrototype;

        protected override HandView CreateView()
        {
            return new HandView(Level, Cards.View, CardPrototype.View);
        }

    }

}
