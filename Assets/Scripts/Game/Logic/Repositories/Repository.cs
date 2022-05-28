using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class Repository<T> : IRepository<T>, IPresenterRepository<T> where T : class, IEntity
    {
        public event Action<EntityLink<T>, T> OnAdded = delegate { };
        public event Action<EntityLink<T>, T> OnRemoved = delegate { };
        public event Action<EntityLink<T>, T> OnChanged = delegate { };
        public event Action<EntityLink<T>, T, IModelEvent> OnEvent = delegate { };

        private Dictionary<Uid, T> _repository = new ();

        public void Add(T entity)
        {
            if (_repository.ContainsKey(entity.Id))
                throw new Exception("Entity already exist");

            _repository.Add(entity.Id, (T)entity.Copy());
            OnAdded(new EntityLink<T>(this, entity.Id), entity);
        }

        public void Remove(T entity)
        {
            if (!_repository.ContainsKey(entity.Id))
                throw new Exception("Entity not exist");

            _repository.Remove(entity.Id);
            OnRemoved(new EntityLink<T>(this, entity.Id), entity);
        }

        public void Save(T entity)
        {
            if (!_repository.ContainsKey(entity.Id))
                throw new Exception("Entity not exist");

            _repository[entity.Id] = (T)entity.Copy();
            OnChanged(new EntityLink<T>(this, entity.Id), entity);
        }

        public IReadOnlyCollection<EntityLink<T>> Get()
        {
            return _repository.Select(x => new EntityLink<T>(this, x.Key)).AsReadOnly();
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


        IReadOnlyCollection<T> IRepository<T>.Get()
        {
            return GetAll();
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return _repository.Select(x => (T)x.Value.Copy()).AsReadOnly();
        }

        public void FireEvent(T entity, IModelEvent modelEvent)
        {
            if (!_repository.ContainsKey(entity.Id))
                throw new Exception("Entity not exist");

            OnEvent(new EntityLink<T>(this, entity.Id), entity, modelEvent);
        }
    }
}
