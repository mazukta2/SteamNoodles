using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class MainScreenUnityView : ScreenUnityView<MainScreenView>
    {
        [SerializeField] HandUnityView _hand;

        private MainScreenView _viewPresenter;
        public override MainScreenView GetView() => _viewPresenter;
        protected override void CreatedInner()
        {
            _hand.ForceAwake();
            _viewPresenter = new MainScreenView(Level, _hand.GetView());
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }
}
