using System;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Fields
{
    public class FieldRepository : CommonSingletonRepository<FieldEntity, Field>
    {
        private readonly IDatabase<ConstructionEntity> _constructions;

        public FieldRepository(ISingletonDatabase<FieldEntity> database, IDatabase<ConstructionEntity> constructions) : base(database)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
        }
        
        protected override Field HandleAdding(FieldEntity entity)
        {
            return new Field(entity, _constructions);
        }
    }
}