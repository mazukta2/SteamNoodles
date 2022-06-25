using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public interface IQuery<out T> : IDisposable where T : class
    {
        event Action<T> OnAdded;
        event Action<T> OnRemoved;
        event Action<T> OnAny;
        event Action<T> OnChanged;
        event Action<T, IModelEvent> OnEvent;
        IReadOnlyCollection<T> Get();
        T Get(Uid uid);
        ISingleQuery<T> GetAsQuery(Uid uid);
    }
}
