﻿using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class PointsOnBuildingService : Disposable, IService
    {
        private readonly IRepository<Construction> _constructions;
        private readonly BuildingPointsService _pointsService;
        private readonly Field _Field;

        public PointsOnBuildingService(IRepository<Construction> constructions, 
            BuildingPointsService pointsService, Field Field)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _pointsService = pointsService ?? throw new ArgumentNullException(nameof(pointsService));
            _Field = Field ?? throw new ArgumentNullException(nameof(Field));
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
            
            _pointsService.Change(buildedByPlayerEvent.Points, _Field.GetWorldPosition(construction));
        }
    }
}
