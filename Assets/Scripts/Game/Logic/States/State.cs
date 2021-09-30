using Game.Assets.Scripts.Game.Logic.Models.Events;
using Game.Assets.Scripts.Game.Logic.States.Game.Level;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.States
{
    public class State
    {
        private Dictionary<uint, IStateEntity> _entities = new Dictionary<uint, IStateEntity>();
        private uint _lastId = 1;
        private WeakEvent _onAdded = new WeakEvent();

        public T GetById<T>(uint id) where T : IStateEntity
        {
            if (!_entities.ContainsKey(id))
                throw new Exception("Non existing entity");

            return (T)_entities[id];
        }

        public void Change<T>(uint id, Action<T> p) where T : IStateEntity
        {
            if (!_entities.ContainsKey(id))
                throw new Exception("Changes in non existing entity");

            var entity = (T)_entities[id];
            p(entity);
            _entities[id] = entity;
        }

        public StateLink<T>[] GetAll<T>() where T : IStateEntity
        {
            var list = new List<StateLink<T>>();
            foreach (var item in _entities)
            {
                if (item.Value is T value)
                    list.Add(new StateLink<T>(this, value.Id));
            }
            return list.ToArray();
        }

        public StateLink<T> Get<T>() where T : IStateEntity
        {
            foreach (var item in _entities)
            {
                if (item.Value is T value)
                    return new StateLink<T>(this, value.Id);
            }
            return null;
        }

        public StateLink<T> Get<T>(uint id) where T : IStateEntity
        {
            return new StateLink<T>(this, id);
        }

        public StateLink<T> Add<T>(Func<State, uint, T> p) where T : IStateEntity
        {
            var id = _lastId + 1;
            var state = p(this, id);
            _entities.Add(id, state);
            _lastId = id;
            _onAdded.Execute<T>(state);
            return new StateLink<T>(this, state.Id);
        }

        public void Update(IStateEntity state)
        {
            _entities[state.Id] = state;
        }

        public void SubscribeToNew<T>(Action<T> p) where T : IStateEntity
        {
            _onAdded.Subscribe(p);
        }

    }
}
