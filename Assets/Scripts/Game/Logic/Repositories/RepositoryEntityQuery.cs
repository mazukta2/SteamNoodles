using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class RepositoryEntityQuery<T> : Disposable, ISingleQuery<T> where T : class, IEntity
    {
        private readonly IRepository<T> _repository;
        private readonly Uid _id;

        public event Action OnAdded = delegate {  };
        public event Action OnRemoved = delegate {  };
        public event Action OnChanged = delegate {  };
        public event Action<IModelEvent> OnEvent = delegate {  };

        public RepositoryEntityQuery(IRepository<T> repository, Uid id)
        {
            _repository = repository;
            _id = id;

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
            return _repository.Get(_id);
        }

        public bool Has()
        {
            return _repository.Has(_id);
        }
        private void HandleOnEvent(T entity, IModelEvent arg2)
        {
            if (entity.Id != _id)
                return;

            OnEvent(arg2);
        }

        private void HandleOnRemoved(T entity)
        {
            if (entity.Id != _id)
                return;
            
            OnRemoved();
        }

        private void HandleOnAdded(T entity)
        {
            if (entity.Id != _id)
                return;
            
            OnAdded();
        }

        private void HadleOnChanged(T entity)
        {
            if (entity.Id != _id)
                return;

            OnChanged();
        }

    }
}