using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Managing
{
    public class ViewsCollection : IViewsCollection, IService
    {
        public event Action OnDispose = delegate { };

        public IReadOnlyCollection<IView> Views => _views.AsReadOnly();

        public int Count => _views.Count;

        private List<IView> _views = new List<IView>();

        public void Dispose()
        {
            OnDispose();

            if (_views.Count > 0)
                throw new Exception("View presenters are not destroyed");
        }

        public void Clear()
        {
            foreach (var item in _views.ToList())
                item.Dispose();
            _views.Clear();
        }

        public T FindView<T>(bool recursively = true) where T : IView
        {
            return FindViews<T>(recursively).FirstOrDefault();
        }

        public IReadOnlyCollection<T> FindViews<T>(bool recursively = true) where T : IView
        {
            var result = _views.ToList().OfType<T>().ToList();

            if (recursively)
            {
                foreach (var collection in _views.OfType<IViewsCollection>())
                {
                    result.AddRange(collection.FindViews<T>(recursively));
                }
            }

            return result.AsReadOnly();
        }

        public void Remove(IView view)
        {
            if (view == null)
                throw new Exception("View can't be null");
            _views.Remove(view);
            OnDispose -= view.Dispose;
        }

        public void Add(IView view)
        {
            if (view == null)
                throw new Exception("View can't be null");
            _views.Add(view);
            OnDispose += view.Dispose;
        }
    }
}
