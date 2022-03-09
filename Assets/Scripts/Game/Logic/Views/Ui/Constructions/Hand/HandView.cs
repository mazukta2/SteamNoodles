using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandView : View
    {
        public ViewContainer Cards;
        public ViewPrototype CardPrototype;

        private HandViewPresenter _viewPresenter;
        protected override void CreatedInner()
        {
            _viewPresenter = new HandViewPresenter(Level, Cards.ViewPresenter, CardPrototype.ViewPresenter);
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }

    public class HandViewPresenter : ViewPresenter
    {
        public ContainerViewPresenter Cards { get; private set; }
        public PrototypeViewPresenter CardPrototype { get; private set; }

        private HandPresenter _presenter;
        public HandViewPresenter(ILevel level, ContainerViewPresenter cards, PrototypeViewPresenter cardPrototype) : base(level)
        {
            Cards = cards ?? throw new ArgumentNullException(nameof(cards));
            CardPrototype = cardPrototype ?? throw new ArgumentNullException(nameof(cardPrototype));

            _presenter = new HandPresenter(level.Model.Hand, this);
        }
    }
}
