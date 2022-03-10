using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionUnityView : UnityView<HandConstructionView>
    {
        public ButtonUnityView Button;

        private HandConstructionView _viewPresenter;

        protected override HandConstructionView CreateView()
        {
            return new HandConstructionView(Level, Button.View);
        }
    }

}
