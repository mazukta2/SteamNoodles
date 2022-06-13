using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using GameUnity.Assets.Scripts.Unity.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Assets.Scripts.Game.Unity.Views
{
    public abstract class UnityPresenterView : MonoBehaviour, 
        IViewWithPresenter,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        public event Action OnDispose = delegate { };
        public bool IsHighlihgted { get; private set; }
        public event Action OnHighlihgtedEnter = delegate { };
        public event Action OnHighlihgtedExit = delegate { };

        public IViewsCollection Collection { get; private set; }
        public bool IsDisposed { get; private set; }

        private List<IPresenter> _presenters = new List<IPresenter>();

        protected void Awake()
        {
            Collection = LevelsManager.Collection;
            PreAwake();
            Collection.Add(this);
        }

        protected virtual void PreAwake()
        {
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

        private void DisposeInner()
        {
            if (IsDisposed)
                return;

            Collection.Remove(this);
            IsDisposed = true;
            OnDispose();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsHighlihgted = true;
            OnHighlihgtedEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsHighlihgted = false;
            OnHighlihgtedExit();
        }

        public void AddPresenter(IPresenter presenter)
        {
            _presenters.Add(presenter);
        }
    }

}