using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Entities;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        public event Action<T> OnAdded = delegate { };
        public event Action<T> OnRemoved = delegate { };
        public event Action<T, IModelEvent> OnEvent = delegate { };

        public int Count => _repository.Count;

        private Dictionary<Uid, T> _repository = new ();

        public Repository()
        {
        }

        public virtual T Add(T entity)
        {
            if (_repository.ContainsKey(entity.Id))
                throw new Exception("Entity already exist");

            _repository.Add(entity.Id, entity);
            entity.OnEvent += HandleEvent;
            
            FireOnAdded(entity);

            return entity;
        }

        public void Remove(T entity)
        {
            if (!_repository.ContainsKey(entity.Id))
                throw new Exception("Entity not exist");

            _repository.Remove(entity.Id);
            entity.OnEvent -= HandleEvent;
            FireOnRemoved(entity);
        }

        public T Get(Uid uid)
        {
            if (!_repository.ContainsKey(uid))
                return null;

            return _repository[uid];
        }

        public void Clear()
        {
            foreach (var item in _repository.ToList())
                Remove(item.Value);
        }

        public IReadOnlyCollection<T> Get()
        {
            return _repository.Select(x => x.Value).AsReadOnly();
        }

        public void FireEvent(T entity, IModelEvent modelEvent)
        {
            if (!_repository.ContainsKey(entity.Id))
                throw new Exception("Entity not exist");

            FireOnModelEvent(entity, modelEvent);
        }

        public bool Has(T entity)
        {
            return _repository.ContainsKey(entity.Id);
        }

        public ISingleQuery<T> GetAsQuery(Uid id)
        {
            return new RepositoryEntityQuery<T>(this, id);
        }

        public IQuery<T> AsQuery()
        {
            return new RepositoryQuery<T>(this);
        }

        public bool Has(Uid id)
        {
            return _repository.ContainsKey(id);
        }

        protected virtual void FireOnAdded(T entity)
        {
            OnAdded(entity);
        }
        
        protected virtual void FireOnRemoved(T entity)
        {
            OnRemoved(entity);
        }
        
        protected virtual void FireOnModelEvent(T entity, IModelEvent modelEvent)
        {
            OnEvent(entity, modelEvent);
        }
        
        private void HandleEvent(IEntity entity, IModelEvent modelEvent)
        {
            FireOnModelEvent((T)entity, modelEvent);
        }

    }
}
