using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Aggregations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Entities;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories.Aggregations
{
    public class AggregationRepository<T> : IAggregationRepository<T> where T : class, IAggregation
    {
        public event Action<T> OnAdded = delegate { };
        public event Action<T> OnRemoved = delegate { };

        public int Count => _repository.Count;

        private Dictionary<Uid, T> _repository = new ();

        public AggregationRepository()
        {
        }

        public virtual T Add(T entity)
        {
            if (_repository.ContainsKey(entity.Id))
                throw new Exception("Entity already exist");

            _repository.Add(entity.Id, entity);
            
            FireOnAdded(entity);

            return entity;
        }

        public void Remove(T entity)
        {
            if (!_repository.ContainsKey(entity.Id))
                throw new Exception("Entity not exist");

            _repository.Remove(entity.Id);
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

        public bool Has(T entity)
        {
            return _repository.ContainsKey(entity.Id);
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

    }
}
