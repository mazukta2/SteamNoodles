﻿using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Resources.Points;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions
{
    public class PointsOnBuildingService : Disposable, IService
    {
        private readonly IDatabase<ConstructionEntity> _constructions;
        private readonly BuildingPointsService _pointsService;

        public PointsOnBuildingService(IDatabase<ConstructionEntity> constructions, 
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

        private void HandleEvent(ConstructionEntity constructionEntity, IModelEvent e)
        {
            if (e is not ConstructionBuiltByPlayerEvent buildedByPlayerEvent)
                return;
            
            _pointsService.Change(buildedByPlayerEvent.Points, constructionEntity.GetWorldPosition());
        }
    }
}
