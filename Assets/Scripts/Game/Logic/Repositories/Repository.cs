using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class Repository<T> : IRepository<T>, IQuery<T> where T : class, IEntity
    {
        public event Action<T> OnAdded = delegate { };
        public event Action<T> OnRemoved = delegate { };
        public event Action<T> OnChanged = delegate { };
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

            _repository.Add(entity.Id, (T)entity.Copy());
            OnAdded(entity);

            return entity;
        }

        public void Remove(T entity)
        {
            if (!_repository.ContainsKey(entity.Id))
                throw new Exception("Entity not exist");

            _repository.Remove(entity.Id);
            OnRemoved(entity);
        }

        public void Save(T entity)
        {
            if (!_repository.ContainsKey(entity.Id))
                throw new Exception("Entity not exist");

            var events = entity.GetEvents().ToArray();
            entity.Clear();

            _repository[entity.Id] = (T)entity.Copy();
            OnChanged(entity);

            foreach (var evt in events)
                FireEvent(entity, evt);
        }

        public T Get(Uid uid)
        {
            if (!_repository.ContainsKey(uid))
                return null;

            return (T)_repository[uid].Copy();
        }

        public void Clear()
        {
            foreach (var item in _repository.ToList())
                Remove(item.Value);
        }

        public IReadOnlyCollection<T> Get()
        {
            return _repository.Select(x => (T)x.Value.Copy()).AsReadOnly();
        }

        public void FireEvent(T entity, IModelEvent modelEvent)
        {
            if (!_repository.ContainsKey(entity.Id))
                throw new Exception("Entity not exist");

            OnEvent(entity, modelEvent);
        }

        public bool Has(T entity)
        {
            return _repository.ContainsKey(entity.Id);
        }

        public ISingleQuery<T> GetAsQuery(Uid id)
        {
            return new RepositoryEntityQuery<T>(this, id);
        }

        public bool Has(Uid id)
        {
            return _repository.ContainsKey(id);
        }
    }
}
