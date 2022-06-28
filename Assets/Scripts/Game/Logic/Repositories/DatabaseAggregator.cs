using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class DatabaseAggregator<TEntity, TAggregator> : Disposable 
        where TEntity : class, IEntity
        where TAggregator : IDisposable
    {
        public Action<TAggregator> OnAdded = delegate {  };
        public Action<Uid> OnRemoved = delegate {  };
        
        private readonly IDatabase<TEntity> _database;
        private readonly Func<TEntity, TAggregator> _add;
        private readonly Dictionary<Uid, TAggregator> _aggregators = new ();

        public DatabaseAggregator(IDatabase<TEntity> database, 
            Func<TEntity, TAggregator> add)
        {
            _database = database;
            _add = add;
            foreach (var entity in _database.Get())
                Add(entity);

            _database.OnAdded += Add;
            _database.OnRemoved += Remove;
        }

        protected override void DisposeInner()
        {
            _database.OnAdded -= Add;
            _database.OnRemoved -= Remove;

            foreach (var aggregator in _aggregators)
                aggregator.Value.Dispose();
            
            _aggregators.Clear();
            
            base.DisposeInner();
        }

        private void Add(TEntity entity)
        {
            var a = _add(entity);
            _aggregators.Add(entity.Id, a);
            OnAdded(a);
        }
        
        private void Remove(TEntity obj)
        {
            var aggregator = _aggregators[obj.Id];
            aggregator.Dispose();
            _aggregators.Remove(obj.Id);
            OnRemoved(obj.Id);
        }

        public IReadOnlyCollection<TAggregator> Get()
        {
            return _aggregators.Values;
        }
        
        public TAggregator Get(Uid uid)
        {
            return _aggregators[uid];
        }
    }
}