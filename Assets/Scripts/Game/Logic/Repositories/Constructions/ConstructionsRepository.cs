using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories.Constructions
{
    public class ConstructionsRepository : CommonRepository<ConstructionEntity, Construction>
    {
        private readonly GameAssetsService _assets;

        public ConstructionsRepository(IDatabase<ConstructionEntity> database, GameAssetsService assets) : base(database)
        {
            _assets = assets;
        }

        protected override Construction HandleAdding(ConstructionEntity entity)
        {
            return new Construction(entity, _assets);
        }
    }
}