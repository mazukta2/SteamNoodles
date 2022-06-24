using Game.Assets.Scripts.Game.Logic.Models.Entities;
using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class SingletonRepository<T> : Repository<T>, ISingletonRepository<T> where T : class, IEntity
    {
        public SingletonRepository()
        {
        }

        public SingletonRepository(T entity)
        {
            base.Add(entity);
        }

        public new event Action OnAdded = delegate {  };
        public new event Action OnRemoved = delegate {  };
        public new event Action OnChanged = delegate {  };
        public new event Action<IModelEvent> OnEvent = delegate {  };

        public override T Add(T entity)
        {
            if (Count != 0)
                throw new Exception("Already has entity");
            return base.Add(entity);
        }

        public void Remove()
        {
            if (Count == 0)
                throw new Exception("No entity");
            
            base.Remove(Get());
        }

        public new T Get()
        {
            if (Count == 0)
                return null;

            return base.Get().First();
        }

        public bool Has()
        {
            return (Count != 0);
        }

        public ISingleQuery<T> AsQuery()
        {
            return new SingleRepositoryEntityQuery<T>(this);
        }

        protected override void FireOnAdded(T entity)
        {
            base.FireOnAdded(entity);
            OnAdded();
        }

        protected override void FireOnRemoved(T entity)
        {
            base.FireOnRemoved(entity);
            OnRemoved();
        }

        protected override void FireOnChanged(T entity)
        {
            base.FireOnChanged(entity);
            OnChanged();
        }

        protected override void FireOnModelEvent(T entity, IModelEvent modelEvent)
        {
            base.FireOnModelEvent(entity, modelEvent);
            OnEvent(modelEvent);
        }
    }
}
