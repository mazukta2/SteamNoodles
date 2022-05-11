using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandUnityView : UnityView<HandPresenter>, IHandView
    {
        [SerializeField] private ContainerUnityView _cards;
        [SerializeField] private PrototypeUnityView _cardPrototype;
        [SerializeField] private ButtonUnityView _cancel;
        [SerializeField] private UnityAnimator _animator;

        public IButton CancelButton => _cancel;
        public IAnimator Animator => _animator;
        public IViewContainer Cards => _cards;
        public IViewPrefab CardPrototype => _cardPrototype;
    }

}
