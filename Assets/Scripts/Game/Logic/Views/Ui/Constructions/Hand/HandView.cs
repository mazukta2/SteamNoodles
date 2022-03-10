using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandView : View<HandViewPresenter>
    {
        public ViewContainer Cards;
        public ViewPrototype CardPrototype;

        private HandViewPresenter _viewPresenter;
        public override HandViewPresenter GetViewPresenter() => _viewPresenter;
    
        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }

        public void Init(ScreenManagerViewPresenter screenManager)
        {
            Cards.ForceAwake();
            CardPrototype.ForceAwake();
            _viewPresenter = new HandViewPresenter(Level, screenManager, Cards.ViewPresenter, CardPrototype.ViewPresenter);
        }
    }

}
