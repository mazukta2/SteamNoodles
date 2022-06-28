using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Assets;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Fields
{
    public class FieldConstructionsRepository : CommonRepository<ConstructionEntity, FieldConstruction>
    {
        private readonly GameAssetsService _assets;

        public FieldConstructionsRepository(IDatabase<ConstructionEntity> database) : base(database)
        {
        }

        protected override FieldConstruction HandleAdding(ConstructionEntity entity)
        {
            return new FieldConstruction(entity);
        }
    }
}