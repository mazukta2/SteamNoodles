using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public interface IQuery<out T> : IEntityList<T>, IDisposable where T : class
    {
        event Action<T> OnAdded;
        event Action<T> OnRemoved;
        event Action<T, IModelEvent> OnEvent;
        ISingleQuery<T> GetAsQuery(Uid uid);
    }
}
