using System;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public interface IDataQuery<T> : IDisposable
    {
        event Action OnAdded;
        event Action OnRemoved;
        event Action<IModelEvent> OnEvent;
        
        T Get();
        bool Has();
    }
}