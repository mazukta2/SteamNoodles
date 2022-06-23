using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class SingletonRepository<T> : ISingletonRepository<T>, IPresenterRepository<T> where T : class, IEntity
    {
        public event Action<T> OnAdded = delegate { };
        public event Action<T> OnRemoved = delegate { };
        public event Action<T> OnChanged = delegate { };
        public event Action<T, IModelEvent> OnEvent = delegate { };

        private Uid _uid;
        private T _value;

        public SingletonRepository()
        {
        }

        public SingletonRepository(T entity)
        {
            Add(entity);
        }

        public void Add(T entity)
        {
            if (_value != null)
                throw new Exception("Entity already exist");

            _uid = entity.Id;
            _value = (T)entity.Copy();
            OnAdded(entity);
        }

        public void Remove(T entity)
        {
            if (_value == null)
                throw new Exception("Entity not exist");

            _uid = null;
            _value = null;
            OnRemoved(entity);
        }

        public void Save(T entity)
        {
            if (_value == null)
                throw new Exception("Entity not exist");

            if (_uid.Value != entity.Id.Value)
                throw new Exception("Wrong id");

            _value = (T)entity.Copy();

            OnChanged(entity);
        }

        IReadOnlyCollection<T> IPresenterRepository<T>.Get()
        {
            return new [] { _value }.AsReadOnly();
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

        public  T Get()
        {
            return _value;
        }

        public bool Has()
        {
            return (_value != null);
        }
    }
}
