using System;
using System.Collections.Generic;
using System.Text;
using Game.Assets.Scripts.Game.Logic.Models;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Managing
{
    public interface IViews : IDisposable
    {
        event Action OnDispose;

        public static IViews Default { get; set; }

        public T FindView<T>(bool recursively = true) where T : IView;
        public IReadOnlyCollection<T> FindViews<T>(bool recursively = true) where T : IView;
        public void Remove(IView view);
        public void Add(IView view);

    }
}
