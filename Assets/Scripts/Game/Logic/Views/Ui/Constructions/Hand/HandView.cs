using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandView : View
    {
        public ViewContainer Cards;
        public ViewPrototype CardPrototype;

        private HandPresenter _presenter;
        protected override void CreatedInner()
        {
            _presenter = new HandPresenter(Level.Model.Hand, this);
        }

        protected override void DisposeInner()
        {
        }

        public void OnValidate()
        {
            if (Cards == null) throw new ArgumentNullException(nameof(Cards));
            if (CardPrototype == null) throw new ArgumentNullException(nameof(CardPrototype));
        }
    }
}
