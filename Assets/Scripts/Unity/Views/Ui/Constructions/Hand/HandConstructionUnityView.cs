using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionUnityView : UnityView<HandConstructionPresenter>, IHandConstructionView
    {
        [SerializeField] ButtonUnityView _button;
        [SerializeField] ImageUnityView _image;

        public IButton Button => _button;
        public IImage Image => _image;
    }

}
