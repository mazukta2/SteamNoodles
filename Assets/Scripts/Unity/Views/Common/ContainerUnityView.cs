using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class ContainerUnityView : UnityView<ContainerView>
    {
        [SerializeField] Transform _pointer;
        public ContainerView ViewPresenter { get; private set; }
        public override ContainerView GetView() => ViewPresenter;

        protected override void CreatedInner()
        {
            ViewPresenter = new ContainerView(Level);
            _containers.Add(this);
        }

        protected override void DisposeInner()
        {
            ViewPresenter.Dispose();
            _containers.Remove(this);
        }

        public Transform GetPointer() => _pointer;

        public static ContainerUnityView Find(ContainerView conteiner)
        {
            return _containers.First(x => x.ViewPresenter == conteiner);
        }

        private static List<ContainerUnityView> _containers = new List<ContainerUnityView>();

        private void HandleOnClear()
        {
            foreach (Transform item in _pointer)
            {
                GameObject.Destroy(item.gameObject);
            }
        }
    }
}
