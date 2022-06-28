using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Aggregations;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class AggregationRepositorySingle<T> : AggregationRepository<T>, 
        IAggregationRepositorySingle<T> where T : class, IAggregation
    {
        public AggregationRepositorySingle()
        {
        }

        public AggregationRepositorySingle(T entity)
        {
            base.Add(entity);
        }

        public new event Action OnAdded = delegate {  };
        public new event Action OnRemoved = delegate {  };

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
    }
}
