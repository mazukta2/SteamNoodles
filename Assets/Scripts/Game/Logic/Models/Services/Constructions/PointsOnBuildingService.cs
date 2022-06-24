using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class PointsOnBuildingService : Disposable, IService
    {
        private readonly IRepository<Construction> _constructions;
        private readonly BuildingPointsService _pointsService;

        public PointsOnBuildingService(IRepository<Construction> constructions, 
            BuildingPointsService pointsService)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _pointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
            _constructions.OnEvent += HandleEvent;
        }

        protected override void DisposeInner()
        {
            _constructions.OnEvent -= HandleEvent;
        }

        private void HandleEvent(Construction construction, IModelEvent e)
        {
            if (e is not ConstructionBuiltByPlayerEvent buildedByPlayerEvent)
                return;
            
            _pointsService.Change(buildedByPlayerEvent.Points, construction.GetWorldPosition());
        }
    }
}
