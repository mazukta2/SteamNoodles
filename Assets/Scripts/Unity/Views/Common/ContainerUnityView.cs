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

        protected override ContainerView CreateView()
        {
            return new ContainerView(Level);
        }

        protected override void CreatedInner()
        {
            _containers.Add(this);
        }

        protected override void DisposeInner()
        {
            _containers.Remove(this);
        }

        public Transform GetPointer() => _pointer;

        public static ContainerUnityView Find(ContainerView conteiner)
        {
            return _containers.First(x => x.View == conteiner);
        }

        private static List<ContainerUnityView> _containers = new List<ContainerUnityView>();

    }
}
