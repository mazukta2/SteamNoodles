using System;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Building
{
    public class GhostRepository : CommonSingletonRepository<GhostEntity, BuildingGhost>
    {
        private readonly IDatabase<ConstructionEntity> _constructions;
        private readonly ISingletonDatabase<FieldEntity> _field;
        private readonly IDatabase<ConstructionCardEntity> _cards;

        public GhostRepository(ISingletonDatabase<GhostEntity> database, 
            IDatabase<ConstructionEntity> constructions, 
            IDatabase<ConstructionCardEntity> constructionCards, 
            ISingletonDatabase<FieldEntity> field) : base(database)
        {
            _cards = constructionCards ?? throw new ArgumentNullException(nameof(constructionCards));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _field = field ?? throw new ArgumentNullException(nameof(field));
        }

        public void Add(Uid constructionCardId)
        {
            var card = _cards.Get(constructionCardId);
            var entity = new GhostEntity(card);
            Database.Add(entity);
        }
        
        public BuildingGhost AddAndGet(Uid constructionCardId)
        {
            Add(constructionCardId);
            return Get();
        }
        
        public new void Remove()
        {
            base.Remove();
        }

        protected override BuildingGhost HandleAdding(GhostEntity entity)
        {
            return new BuildingGhost(entity, _constructions, _field.Get());
        }
    }
}