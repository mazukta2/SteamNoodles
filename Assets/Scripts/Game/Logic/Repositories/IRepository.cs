﻿using System;
using Game.Assets.Scripts.Game.Logic.Entities;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
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
    }
}
