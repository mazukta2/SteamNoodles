using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Building
{
    public class BuildingConstructionsRepository : CommonRepository<ConstructionEntity, BuildingConstruction>
    {

        public BuildingConstructionsRepository(IDatabase<ConstructionEntity> database) : base(database)
        {
        }

        protected override BuildingConstruction HandleAdding(ConstructionEntity entity)
        {
            return new BuildingConstruction(entity);
        }
    }
}