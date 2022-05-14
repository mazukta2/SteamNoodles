using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views
{
    public abstract class UnitySimpleView : MonoBehaviour, IView
    {
        public event Action OnDispose = delegate { };
        public event Action OnHighlihgtedEnter = delegate { };
        public event Action OnHighlihgtedExit = delegate { };

        public IViewsCollection Collection { get; private set; }
        public bool IsDisposed { get; private set; }
        public bool IsHighlihgted { get; private set; }

        protected void Awake()
        {
            Collection = ILevelsManager.Default.Collection;
            PreAwake();
            Collection.Add(this);
        }

        protected void OnDestroy()
        {
            DisposeInner();
        }

        public void Dispose()
        {
            DisposeInner();
            Destroy(gameObject);
        }

        protected virtual void PreAwake()
        {
        }

        private void DisposeInner()
        {
            if (IsDisposed)
                return;

            Collection.Remove(this);
            IsDisposed = true;
            OnDispose();
        }
    }
}