using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public abstract class CommonRepository<TEntity, TAggregator> : Disposable, IService 
        where TEntity : class, IEntity
        where TAggregator : IDisposable
    {
        public Action<TAggregator> OnAdded = delegate {  };
        public Action<TAggregator> OnRemoved = delegate {  };

        protected IDatabase<TEntity> Database => _database;
        
        private readonly IDatabase<TEntity> _database;
        private readonly DatabaseAggregator<TEntity, TAggregator> _aggregations;

        public CommonRepository(IDatabase<TEntity> database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _aggregations = new(_database, HandleAdding);
            _aggregations.OnAdded += HandleAdded;
            _aggregations.OnRemoved += HandleRemoved;
        }

        protected override void DisposeInner()
        {
            _aggregations.OnAdded -= HandleAdded;
            _aggregations.OnRemoved -= HandleRemoved;
            _aggregations.Dispose();
        }

        public IReadOnlyCollection<TAggregator> Get()
        {
            return _aggregations.Get();
        }
        
        public TAggregator Get(Uid id)
        {
            return _aggregations.Get(id);
        }
        
        public bool Has(Uid id)
        {
            return _database.Has(id);
        }

        protected void Remove(Uid id)
        {
            _database.Remove(id);
        }

        protected abstract TAggregator HandleAdding(TEntity entity);
        
        private void HandleRemoved(TAggregator obj)
        {
            OnRemoved(obj);
        }

        private void HandleAdded(TAggregator obj)
        {
            OnAdded(obj);
        }


    }
}