using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Managing
{
    public interface IViewsCollection
    {
        event Action OnDispose;
        void Dispose();
        public T FindView<T>(bool recursively = true) where T : IView;
        public IReadOnlyCollection<T> FindViews<T>(bool recursively = true) where T : IView;
        public void Remove(IView view);
        public void Add(IView view);

    }
}
