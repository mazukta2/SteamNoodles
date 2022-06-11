using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Events;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Requests
{
    public class GhostRequestsService : Disposable, IService
    {
        public event Action OnUpdate = delegate { };

        private readonly FieldService _fieldService;
        private readonly ConstructionsService _constructionsService;
        private readonly BuildingModeService _modeService;
        private readonly IEvents _events;
        private Subscriber<GhostStateChangedEvent> _onGhostChanged;
        private Subscriber<GhostPositionChangedEvent> _onGhostPositionChanged;

        public GhostRequestsService(FieldService fieldService,
            ConstructionsService constructionsService, BuildingModeService modeService, IEvents events)
        {
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _modeService = modeService ?? throw new ArgumentNullException(nameof(modeService));
            _events = events;

            _onGhostChanged = _events.Get<GhostStateChangedEvent>(HandleModeOnChanged);
            _onGhostPositionChanged = _events.Get<GhostPositionChangedEvent>(HandleOnPositionChanged);
        }

        protected override void DisposeInner()
        {
            _onGhostChanged.Dispose();
            _onGhostPositionChanged.Dispose();
        }

        public GhostModel Get()
        {
            return new GhostModel(this);
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

    }
}
