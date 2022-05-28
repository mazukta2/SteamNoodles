using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.Constructions
{
    public class ConstructionDestroyedOnWaveEndEvent : IModelEvent
    {
        private readonly IRepository<Construction> _repository;
        private Construction _construction;

        public ConstructionDestroyedOnWaveEndEvent(IRepository<Construction> repository, Construction construction)
        {
            _repository = repository;
            _construction = construction;
        }

        public void Fire()
        {
            _repository.FireEvent(_construction, this);
        }
    }
}
