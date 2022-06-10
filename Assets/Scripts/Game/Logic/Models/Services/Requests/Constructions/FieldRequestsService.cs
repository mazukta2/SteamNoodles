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
    public class FieldRequestsService : Disposable, IService
    {
        public event Action OnUpdate = delegate { };

        private readonly FieldService _fieldService;
        private readonly ConstructionsService _constructionsService;
        private readonly BuildingModeService _modeService;
        private readonly IEvents _events;
        private Subscriber<GhostStateChangedEvent> _onGhostChanged;
        private Subscriber<GhostPositionChangedEvent> _onGhostPositionChanged;
        private Subscriber<EntityAddedToRepositoryEvent<Construction>> _added;
        private Subscriber<EntityRemovedFromRepositoryEvent<Construction>> _removed;

        public FieldRequestsService(FieldService fieldService,
            ConstructionsService constructionsService, BuildingModeService modeService, IEvents events)
        {
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _modeService = modeService ?? throw new ArgumentNullException(nameof(modeService));
            _events = events;

            _onGhostChanged = _events.Get<GhostStateChangedEvent>(HandleModeOnChanged);
            _onGhostPositionChanged = _events.Get<GhostPositionChangedEvent>(HandleOnPositionChanged);
            _added = _events.Get<EntityAddedToRepositoryEvent<Construction>>(HandleConstructionsOnAdded);
            _removed = _events.Get<EntityRemovedFromRepositoryEvent<Construction>>(HandleConstructionsOnRemoved);
        }

        protected override void DisposeInner()
        {
            _added.Dispose();
            _removed.Dispose();
            _onGhostChanged.Dispose();
            _onGhostPositionChanged.Dispose();
        }

        public FieldModel Get()
        {
            return new FieldModel(this);
        }

        public FieldBoundaries GetBoundaries()
        {
            return _fieldService.GetBoundaries();
        }

        public IReadOnlyDictionary<IntPoint, CellModel> GetCells()
        {
            var list = new Dictionary<IntPoint, CellModel>();

            var occupiedCells = _constructionsService.GetAllOccupiedSpace();
            IReadOnlyCollection<FieldPosition> ocuppiedCellsByGhost = null;
            if (_modeService.IsEnabled)
            {
                var scheme = _modeService.Card.Scheme;
                ocuppiedCellsByGhost = scheme.Placement.GetOccupiedSpace(_modeService.GetPosition(), _modeService.GetRotation());
            }

            var occupiedByBuildings = _constructionsService.GetAllOccupiedSpace();
            var boundaries = _fieldService.GetBoundaries();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    var state = CellPlacementStatus.Normal;

                    if (occupiedByBuildings.Any(v => v.Value == new IntPoint(x, y)))
                        state = CellPlacementStatus.IsUnderConstruction;

                    if (_modeService.IsEnabled)
                    {
                        var scheme = _modeService.Card.Scheme;
                        if (_constructionsService.IsFreeCell(scheme, new FieldPosition(x, y), _modeService.GetRotation()))
                            state = CellPlacementStatus.IsReadyToPlace;

                        if (ocuppiedCellsByGhost.Any(v => v.Value == new IntPoint(x, y)))
                        {
                            if (state == CellPlacementStatus.IsReadyToPlace)
                                state = CellPlacementStatus.IsAvailableGhostPlace;
                            else
                                state = CellPlacementStatus.IsNotAvailableGhostPlace;
                        }
                    }
                    list.Add(new IntPoint(x, y),
                        new CellModel(state, 
                            _fieldService.GetWorldPosition(new FieldPosition(new IntPoint(x, y)),
                            new IntRect(0, 0, 1, 1))));
                }
            }

            return list;
        }

        private void HandleChanges()
        {
            OnUpdate();
        }

        private void HandleConstructionsOnRemoved(EntityRemovedFromRepositoryEvent<Construction> obj)
        {
            HandleChanges();
        }

        private void HandleConstructionsOnAdded(EntityAddedToRepositoryEvent<Construction> obj)
        {
            HandleChanges();
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
