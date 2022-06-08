using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Repositories
{
    public interface IRepository<T> : IBaseRepository where T : class, IEntity
    {
        event Action<T> OnModelAdded;
        event Action<T> OnModelRemoved;
        event Action<T> OnModelChanged;
        event Action<T, IModelEvent> OnModelEvent;

        EntityLink<T> Add(T entity);
        void Remove(T entity);
        void Save(T entity);
        IReadOnlyCollection<T> Get();
        int Count { get; }
        void FireEvent(T entity, IModelEvent modelEvent);
        bool Has(T entity);
    }
}
