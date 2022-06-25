using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class RepositoryQuery<T> : Disposable, IQuery<T> where T : class, IEntity
    {
        public event Action<T> OnAdded = delegate {  };
        public event Action<T> OnRemoved= delegate {  };
        public event Action<T> OnAny= delegate {  };
        public event Action<T, IModelEvent> OnEvent= delegate {  };
        
        private readonly IRepository<T> _repository;

        public RepositoryQuery(IRepository<T> repository)
        {
            _repository = repository;

            _repository.OnAdded += HandleOnAdded;
            _repository.OnRemoved += HandleOnRemoved;
            _repository.OnEvent += HandleOnEvent;
        }

        protected override void DisposeInner()
        {
            _repository.OnAdded -= HandleOnAdded;
            _repository.OnRemoved -= HandleOnRemoved;
            _repository.OnEvent -= HandleOnEvent;
        }
        private void HandleOnEvent(T entity, IModelEvent arg2)
        {
            OnEvent(entity, arg2);
            OnAny(entity);
        }
        
        private void HandleOnRemoved(T entity)
        {
            OnRemoved(entity);
            OnAny(entity);
        }
        
        private void HandleOnAdded(T entity)
        {
            OnAdded(entity);
            OnAny(entity);
        }
        
        public IReadOnlyCollection<T> Get()
        {
            return _repository.Get();
        }

        public T Get(Uid uid)
        {
            return _repository.Get(uid);
        }

        public ISingleQuery<T> GetAsQuery(Uid uid)
        {
            return new RepositoryEntityQuery<T>(_repository, uid);
        }
    }
}