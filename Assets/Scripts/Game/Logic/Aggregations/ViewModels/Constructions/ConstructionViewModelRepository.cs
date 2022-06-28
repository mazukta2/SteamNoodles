using System;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Assets;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Constructions
{
    public class ConstructionViewModelRepository : CommonRepository<ConstructionEntity, ConstructionViewModel>
    {
        public Action<ConstructionViewModel> OnFillRequest = delegate {  };

        public ConstructionViewModelRepository(IDatabase<ConstructionEntity> database) : base(database)
        {
        }

        protected override ConstructionViewModel HandleAdding(ConstructionEntity entity)
        {
            var model = new ConstructionViewModel(entity.Id);
            OnFillRequest(model);
            return model;
        }
    }
}