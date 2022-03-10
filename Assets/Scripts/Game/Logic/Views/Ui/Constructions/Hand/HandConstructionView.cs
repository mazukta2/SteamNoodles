using Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionView : View<HandConstructionViewPresenter>
    {
        public ButtonView Button;

        private HandConstructionViewPresenter _viewPresenter;
        public override HandConstructionViewPresenter GetViewPresenter() => _viewPresenter;

        protected override void CreatedInner()
        {
            Button.ForceAwake();
            _viewPresenter = new HandConstructionViewPresenter(Level, Button.ViewPresenter);
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }

}
