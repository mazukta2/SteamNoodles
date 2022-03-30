using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class PrototypeUnityView : MonoBehaviour, IViewPrefab
    {
        private bool _itsOriginal = true;
        public void SetOriginal(bool v)
        {
            _itsOriginal = v;
        }

        protected void Awake()
        {
            if (!_itsOriginal)
                Destroy(this);
            else
                gameObject.SetActive(false);
        }
    }
}
