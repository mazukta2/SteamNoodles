using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Repositories
{
    public interface IRepository<T> : IBaseRepository where T : class, IEntity
    {
        event Action<T> OnAdded;
        event Action<T> OnRemoved;
        event Action<T> OnChanged;
        event Action<T, IModelEvent> OnEvent;

        T Add(T entity);
        void Remove(T entity);
        void Save(T entity);
        IReadOnlyCollection<T> Get();
        T Get(Uid id);
        int Count { get; }
        void FireEvent(T entity, IModelEvent modelEvent);
        bool Has(T entity);
    }
}
