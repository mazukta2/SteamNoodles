using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionUnityView : UnityView<HandConstructionView>
    {
        [SerializeField] ButtonUnityView _button;
        [SerializeField] ImageUnityView _image;

        private HandConstructionView _viewPresenter;

        protected override HandConstructionView CreateView()
        {
            return new HandConstructionView(Level, _button.View, _image.View);
        }
    }

}
