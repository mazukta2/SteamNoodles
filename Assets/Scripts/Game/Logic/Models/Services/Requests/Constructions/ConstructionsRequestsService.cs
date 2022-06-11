using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Common.Services.Events;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
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
        private readonly IEvents _events;
        private readonly IGameControls _controls;
        private readonly IRepository<Construction> _constructions;
        private readonly BuildingModeService _buildingModeService;
        private Subscriber<GhostStateChangedEvent> _onGhostChanged;
        private Subscriber<GhostPositionChangedEvent> _onGhostPositionChanged;

        public ConstructionsRequestsService(IRepository<Construction> constructions, 
            BuildingModeService buildingModeService,
            FieldService fieldService, IGameAssets assets, IEvents events, IGameControls controls)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            _events = events;
            _controls = controls;
            _onGhostChanged = _events.Get<GhostStateChangedEvent>(HandleModeOnChanged);
            _onGhostPositionChanged = _events.Get<GhostPositionChangedEvent>(HandleOnPositionChanged);
            _constructions.OnModelRemoved += Constructions_OnModelRemoved;
        }

        protected override void DisposeInner()
        {
            _constructions.OnModelRemoved -= Constructions_OnModelRemoved;
            _onGhostChanged.Dispose();
            _onGhostPositionChanged.Dispose();
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
                var distance = _buildingModeService.GetTargetPosition().GetDistanceTo(model.WorldPosition);
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
        private void HandleChanges()
        {
            OnUpdate();
        }

        private void HandleOnPositionChanged(GhostPositionChangedEvent obj)
        {
            HandleChanges();
        }

        private void HandleModeOnChanged(GhostStateChangedEvent obj)
        {
            HandleChanges();
        }

        private void Constructions_OnModelRemoved(Construction obj)
        {
            OnRemoved(obj);
        }

    }
}
