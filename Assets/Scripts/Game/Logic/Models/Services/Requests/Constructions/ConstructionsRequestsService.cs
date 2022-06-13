﻿using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Requests
{
    public class ConstructionsRequestsService : Disposable, IService
    {
        public event Action OnUpdate = delegate { };
        public event Action<Construction> OnRemoved = delegate { };

        private readonly FieldService _fieldService;
        private readonly IGameAssets _assets;
        private readonly IGameControls _controls;
        private readonly IRepository<Construction> _constructions;
        private readonly BuildingModeService _buildingModeService;

        public ConstructionsRequestsService(IRepository<Construction> constructions, 
            BuildingModeService buildingModeService,
            FieldService fieldService, IGameAssets assets, IGameControls controls)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));

            _controls = controls;
            _buildingModeService.OnChanged += HandleOnChanged;
            _buildingModeService.OnPositionChanged += HandleOnPositionChanged;
            _constructions.OnModelRemoved += Constructions_OnModelRemoved;
        }

        protected override void DisposeInner()
        {
            _constructions.OnModelRemoved -= Constructions_OnModelRemoved;
            _buildingModeService.OnChanged -= HandleOnChanged;
            _buildingModeService.OnPositionChanged -= HandleOnPositionChanged;
        }

        public ConstructionModel Get(Uid id)
        {
            var construction = _constructions.Get(id);
            return new ConstructionModel(construction, this, _assets);
        }

        public GameVector3 GetWorldPosition(Construction construction)
        {
            return _fieldService.GetWorldPosition(construction);
        }

        public void Shake()
        {
            _controls.ShakeCamera();
        }

        public float GetShrink(ConstructionModel model)
        {
            if (_buildingModeService.IsEnabled)
            {
                var distance = _buildingModeService.GetTargetPosition().GetDistanceTo(model.GetWorldPosition());
                if (distance > model.GhostShrinkDistance)
                    return 1;
                else if (distance > model.GhostHalfShrinkDistance)
                    return distance / model.GhostShrinkDistance;
                else
                    return 0.2f;
            }
            else
            {
                return 1;
            }
        }

        private void HandleOnPositionChanged()
        {
            OnUpdate();
        }

        private void HandleOnChanged(bool obj)
        {
            OnUpdate();
        }

        private void Constructions_OnModelRemoved(Construction obj)
        {
            OnRemoved(obj);
        }

    }
}