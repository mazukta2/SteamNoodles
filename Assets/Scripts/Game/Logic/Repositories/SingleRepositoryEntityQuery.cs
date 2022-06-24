using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class SingleRepositoryEntityQuery<T> : Disposable, ISingleQuery<T> where T : class, IEntity
    {
        private readonly ISingletonRepository<T> _repository;

        public event Action OnAdded = delegate {  };
        public event Action OnRemoved = delegate {  };
        public event Action OnChanged = delegate {  };
        public event Action<IModelEvent> OnEvent = delegate {  };

        public SingleRepositoryEntityQuery(ISingletonRepository<T> repository)
        {
            _repository = repository;

            _repository.OnChanged += HadleOnChanged;
            _repository.OnAdded += HandleOnAdded;
            _repository.OnRemoved += HandleOnRemoved;
            _repository.OnEvent += HandleOnEvent;
        }

        protected override void DisposeInner()
        {
            _repository.OnChanged -= HadleOnChanged;
            _repository.OnAdded -= HandleOnAdded;
            _repository.OnRemoved -= HandleOnRemoved;
            _repository.OnEvent -= HandleOnEvent;
        }

        public T Get()
        {
            return _repository.Get();
        }

        public bool Has()
        {
            return _repository.Has();
        }
        private void HandleOnEvent(T entity, IModelEvent arg2)
        {
            OnEvent(arg2);
        }

        private void HandleOnRemoved(T entity)
        {
            OnRemoved();
        }

        private void HandleOnAdded(T entity)
        {
            OnAdded();
        }

        private void HadleOnChanged(T entity)
        {
            OnChanged();
        }

    }
}