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

        //protected override HandView CreatePresenter()
        //{
        //    return new HandView(Level, Cards, CardPrototype);
        //}

        //public IViewContainer Cards { get; private set; }
        //public IViewPrefab CardPrototype { get; private set; }
        //public HandView(ILevel level, IViewContainer cards, IViewPrefab cardPrototype) : base(level)
        //{
        //    Cards = cards ?? throw new ArgumentNullException(nameof(cards));
        //    CardPrototype = cardPrototype ?? throw new ArgumentNullException(nameof(cardPrototype));
        //}
    }

}
