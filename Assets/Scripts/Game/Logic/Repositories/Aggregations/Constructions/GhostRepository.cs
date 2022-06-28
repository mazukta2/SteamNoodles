using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories.Aggregations.Constructions
{
    public class GhostRepository : Disposable, IService
    {
        public Action<ConstructionGhostPlacing> OnAdded = delegate {  };
        public Action<Uid> OnRemoved = delegate {  };

        private readonly ISingletonDatabase<ConstructionGhost> _database;
        private readonly DatabaseAggregator<ConstructionGhost, ConstructionGhostPlacing> _aggregations;
        private readonly IDatabase<Construction> _constructions;
        private readonly ISingletonDatabase<Field> _field;
        private readonly IDatabase<ConstructionCard> _cards;

        public GhostRepository(ISingletonDatabase<ConstructionGhost> database, 
            IDatabase<Construction> constructions, 
            IDatabase<ConstructionCard> constructionCards, 
            ISingletonDatabase<Field> field)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _cards = constructionCards ?? throw new ArgumentNullException(nameof(constructionCards));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _aggregations = new(_database, Handle);
            _aggregations.OnAdded += HandleAdded;
            _aggregations.OnRemoved += HandleRemoved;
        }

        protected override void DisposeInner()
        {
            _aggregations.OnAdded -= HandleAdded;
            _aggregations.OnRemoved -= HandleRemoved;
            _aggregations.Dispose();
        }

        public void Add(Uid constructionCardId)
        {
            var card = _cards.Get(constructionCardId);
            var entity = new ConstructionGhost(card, _field.Get());
            _database.Add(entity);
        }
        
        public ConstructionGhostPlacing AddAndGet(Uid constructionCardId)
        {
            Add(constructionCardId);
            return Get();
        }

        public ConstructionGhostPlacing Get()
        {
            return _aggregations.Get().First();
        }
        
        public bool Has()
        {
            return _database.Has();
        }

        public void Remove()
        {
            _database.Remove();
        }

        private ConstructionGhostPlacing Handle(ConstructionGhost entity)
        {
            return new ConstructionGhostPlacing(entity, _constructions, _field.Get());
        }
        
        private void HandleRemoved(Uid obj)
        {
            OnRemoved(obj);
        }

        private void HandleAdded(ConstructionGhostPlacing obj)
        {
            OnAdded(obj);
        }


    }
}