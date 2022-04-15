using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandUnityView : UnityView<HandPresenter>, IHandView
    {
        public ContainerUnityView Cards;
        public PrototypeUnityView CardPrototype;

        IViewContainer IHandView.Cards => Cards;
        IViewPrefab IHandView.CardPrototype => CardPrototype;
    }

}
