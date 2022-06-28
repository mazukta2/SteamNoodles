using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Entities;

namespace Game.Assets.Scripts.Game.Logic.Databases
{
    public class SingletonDatabase<T> : Database<T>, ISingletonDatabase<T> where T : class, IEntity
    {
        public SingletonDatabase()
        {
        }

        public SingletonDatabase(T entity)
        {
            base.Add(entity);
        }

        public new event Action OnAdded = delegate {  };
        public new event Action OnRemoved = delegate {  };
        public new event Action<IModelEvent> OnEvent = delegate {  };

        public new SingletonDatabase<T> AddRange(params T[] entities)
        {
            foreach (var entity in entities)
                Add(entity);
            
            return this;
        }
        
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

        protected override void FireOnModelEvent(T entity, IModelEvent modelEvent)
        {
            base.FireOnModelEvent(entity, modelEvent);
            OnEvent(modelEvent);
        }
    }
}
