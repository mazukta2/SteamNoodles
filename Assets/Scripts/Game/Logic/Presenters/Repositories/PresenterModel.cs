using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Repositories
{
    public class PresenterModel<T> : Disposable where T : class
    {
        public event Action OnRemoved = delegate { };
        public event Action OnChanged = delegate { };
        public event Action<IModelEvent> OnEvent = delegate { };

        public PresenterModel(IPresenterRepository<T> repository, Uid id)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _id = id ?? throw new ArgumentNullException(nameof(id));
            _value = repository.Get(id) ?? throw new ArgumentNullException(nameof(id));

            _repository.OnRemoved += HandleOnRemoved;
            _repository.OnChanged += HandleOnChanged;
            _repository.OnEvent += HandleOnEvent;
        }

        protected override void DisposeInner()
        {
            _repository.OnRemoved -= HandleOnRemoved;
            _repository.OnChanged -= HandleOnChanged;
            _repository.OnEvent -= HandleOnEvent;
        }

        private T _value;
        private readonly IPresenterRepository<T> _repository;
        private readonly Uid _id;

        public T Get()
        {
            return _value;
        }

        private void HandleOnRemoved(EntityLink<T> entity, T obj)
        {
            if (entity.Id != _id)
                return;

            _value = null;
            OnRemoved();
        }

        private void HandleOnChanged(EntityLink<T> entity, T obj)
        {
            _value = obj;
            OnChanged();
        }

        private void HandleOnEvent(EntityLink<T> entity, T obj, IModelEvent evt)
        {
            if (entity.Id != _id)
                return;

            OnEvent(evt);
        }

        public bool IsRemoved()
        {
            return _value == null;
        }
    }
}
