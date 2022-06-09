using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services.Events;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class SingletonRepository<T> : ISingletonRepository<T>, IPresenterRepository<T> where T : class, IEntity
    {
        public event Action<EntityLink<T>, T> OnAdded = delegate { };
        public event Action<EntityLink<T>, T> OnRemoved = delegate { };
        public event Action<EntityLink<T>, T> OnChanged = delegate { };
        public event Action<EntityLink<T>, T, IModelEvent> OnEvent = delegate { };

        private Uid _uid;
        private T _value;
        private IEvents _events;

        public SingletonRepository(IEvents events)
        {
            _events = events ?? throw new ArgumentNullException(nameof(events));
        }

        public void Add(T entity)
        {
            if (_value != null)
                throw new Exception("Entity already exist");

            _uid = entity.Id;
            _value = (T)entity.Copy();
            OnAdded(new EntityLink<T>(this, entity.Id), entity);
        }

        public void Remove(T entity)
        {
            if (_value == null)
                throw new Exception("Entity not exist");

            _uid = null;
            _value = null;
            OnRemoved(new EntityLink<T>(this, entity.Id), entity);
        }

        public void Save(T entity)
        {
            if (_value == null)
                throw new Exception("Entity not exist");

            if (_uid.Value != entity.Id.Value)
                throw new Exception("Wrong id");

            _value = (T)entity.Copy();

            OnChanged(new EntityLink<T>(this, entity.Id), entity);
        }

        public IReadOnlyCollection<EntityLink<T>> Get()
        {
            return new [] { new EntityLink<T>(this, _uid) }.AsReadOnly();
        }

        public T Get(Uid uid)
        {
            if (_value == null)
                return null;

            if (_uid.Value != uid.Value)
                throw new Exception("Wrong id");

            return (T)_value.Copy();
        }

        public void Clear()
        {
            _uid = null;
            _value = null;
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return new[] { _value }.AsReadOnly();
        }

        T ISingletonRepository<T>.Get()
        {
            return _value;
        }

        public bool Has()
        {
            return (_value != null);
        }
    }
}
