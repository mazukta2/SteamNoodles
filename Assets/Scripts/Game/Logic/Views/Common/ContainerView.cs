using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{

    public class ContainerView : View
    {
        private List<View> _views = new List<View>();

        public ContainerView(ILevel level) : base(level)
        {
        }

        protected override void DisposeInner()
        {
            Clear();
        }

        public void Clear()
        {
            foreach (var item in _views)
                item.Dispose();
            _views.Clear();
        }

        public T Create<T>(Func<ILevel, T> creator) where T : View
        {
            var viewPresenter = creator(Level);
            _views.Add(viewPresenter);
            return viewPresenter;
        }

        public bool Has<T>()
        {
            return _views.OfType<T>().Any();
        }

        public IReadOnlyCollection<T> Get<T>() where T : View
        {
            return _views.OfType<T>().AsReadOnly();
        }
    }
}
