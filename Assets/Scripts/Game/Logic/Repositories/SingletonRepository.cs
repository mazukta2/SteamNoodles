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
    }
}
