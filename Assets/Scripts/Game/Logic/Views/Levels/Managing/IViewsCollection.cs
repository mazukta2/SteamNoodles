using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Managing
{
    public interface IViewsCollection : IDisposable
    {
        event Action OnDispose;
        public T FindView<T>(bool recursively = true) where T : IView;
        public IReadOnlyCollection<T> FindViews<T>(bool recursively = true) where T : IView;
        public void Remove(IView view);
        public void Add(IView view);
        public int Count { get; }

        public bool Has<T>() where T : IView
        {
            return FindViews<T>().Count > 0;
        }
    }
}
