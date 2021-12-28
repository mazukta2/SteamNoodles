
using Game.Assets.Scripts.Game.Logic.Views;
using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class ViewMonoBehaviour : MonoBehaviour, IView
    {
        public event Action OnDispose = delegate { };
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
            GameObject.Destroy(gameObject);
        }
    }
}