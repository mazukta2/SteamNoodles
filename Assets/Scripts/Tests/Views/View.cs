using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views
{
    public abstract class View : Disposable, IView
    {
        public IViewsCollection Collection { get; private set; }
        public bool IsHighlihgted { get; private set; }
        public event Action OnHighlihgtedEnter = delegate { };
        public event Action OnHighlihgtedExit = delegate { };

        public View(IViewsCollection collection)
        {
            Collection = collection;
            Collection.Add(this);
        }

        protected override void Dispose(bool disposing)
        {
            Collection.Remove(this);
            base.Dispose(disposing);
        }

        public void SetHighlight(bool highlight)
        {
            if (IsHighlihgted == highlight)
                return;

            IsHighlihgted = highlight;
            if (IsHighlihgted)
                OnHighlihgtedEnter();
            else
                OnHighlihgtedExit();
        }
    }
}
