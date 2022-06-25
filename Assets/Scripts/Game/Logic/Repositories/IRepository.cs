using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Repositories
{
    public interface IRepository<T> : IEntityList<T>, IBaseRepository where T : class, IEntity
    {
        event Action<T> OnAdded;
        event Action<T> OnRemoved;
        event Action<T, IModelEvent> OnEvent;

        T Add(T entity);
        void Remove(T entity);
        int Count { get; }
        void FireEvent(T entity, IModelEvent modelEvent);
        bool Has(T entity);
        bool Has(Uid id);
        ISingleQuery<T> GetAsQuery(Uid id);
        IQuery<T> AsQuery();
    }
}
