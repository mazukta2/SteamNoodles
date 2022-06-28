using System;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Ghosts
{
    public class GhostViewModelRepository : CommonSingletonRepository<GhostEntity, GhostViewModel>
    {
        public Action<GhostViewModel> OnFillRequest = delegate {  };
        
        public GhostViewModelRepository(ISingletonDatabase<GhostEntity> database) : base(database)
        {
        }

        protected override GhostViewModel HandleAdding(GhostEntity entity)
        {
            var model = new GhostViewModel(entity.Id);
            OnFillRequest(model);
            return model;
        }
    }
}