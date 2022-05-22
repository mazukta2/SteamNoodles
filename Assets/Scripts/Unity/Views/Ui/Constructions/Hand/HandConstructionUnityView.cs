using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionUnityView : UnityView<HandConstructionPresenter>, IHandConstructionView
    {
        [SerializeField] ButtonUnityView _button;
        [SerializeField] ImageUnityView _image;
        [SerializeField] ContainerUnityView _toolitpContainer;
        [SerializeField] PrototypeUnityView _toolitpPrototype;
        [SerializeField] UnityText _amount;
        [SerializeField] AnimatorUnity _animator;

        public IButton Button => _button;
        public IImage Image => _image;
        public IViewContainer TooltipContainer => _toolitpContainer;
        public IViewPrefab TooltipPrefab => _toolitpPrototype;
        public IText Amount => _amount;
        public IAnimator Animator => _animator;
    }

}
