using Game.Assets.Scripts.Game.Logic.Models.Events;
using Game.Assets.Scripts.Game.Logic.States.Events;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.States
{
    public class State
    {
        private Dictionary<uint, IStateEntity> _entities = new Dictionary<uint, IStateEntity>();
        private uint _lastId = 1;
        private WeakEvent _onAdded = new WeakEvent();

        public T Get<T>(uint id) where T : IStateEntity
        {
            if (!_entities.ContainsKey(id))
                throw new Exception("Non existing entity");

            return (T)_entities[id];
        }

        public void Change<T>(uint id, T entity) where T : IStateEntity
        {
            if (!_entities.ContainsKey(id))
                throw new Exception("Changes in non existing entity");

            _entities[id] = entity;
        }

        public T[] GetAll<T>() where T : IStateEntity
        {
            var list = new List<T>();
            foreach (var item in _entities)
            {
                if (item.Value is T value)
                    list.Add(value);
            }
            return list.ToArray();
        }

        public uint[] GetAllId<T>() where T : IStateEntity
        {
            var list = new List<uint>();
            foreach (var item in _entities)
            {
                if (item.Value is T value)
                    list.Add(item.Key);
            }
            return list.ToArray();
        }

        public Nullable<T> Get<T>() where T : struct, IStateEntity
        {
            foreach (var item in _entities)
            {
                if (item.Value is T value)
                    return new Nullable<T>(value);
            }
            return null;
        }


        public (uint, T) Add<T>(T state) where T : IStateEntity
        {
            var id = _lastId + 1;
            _entities.Add(id, state);
            _lastId = id;
            _onAdded.Execute<T>(id, state);
            return (id, state);
        }

        public void Update(uint id, IStateEntity state)
        {
            _entities[id] = state;
        }

        public void Subscribe<T>(Action<uint, T> p, StateEventType type) where T : IStateEntity
        {
            if (type == StateEventType.Add)
                _onAdded.Subscribe(p);
        }

    }
}
