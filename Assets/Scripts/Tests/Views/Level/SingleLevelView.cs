using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Managing
{
    public class SingleLevelView : ILevelView
    {
        public event Action OnDispose = delegate { };

        private List<IView> _views = new List<IView>();

        public SingleLevelView()
        {
        }

        public void Dispose()
        {
            OnDispose();

            if (_views.Count > 0)
                throw new Exception("View presenters are not destroyed");
        }

        public T FindView<T>() where T : IView
        {
            return _views.ToList().OfType<T>().FirstOrDefault();
        }

        public IReadOnlyCollection<T> FindViews<T>() where T : IView
        {
            return _views.ToList().OfType<T>().AsReadOnly();
        }

        public void Remove(IView view)
        {
            if (view == null)
                throw new Exception("View can't be null");
            _views.Remove(view);
        }

        public void Add(IView view)
        {
            if (view == null)
                throw new Exception("View can't be null");
            _views.Add(view);
        }
    }
}
