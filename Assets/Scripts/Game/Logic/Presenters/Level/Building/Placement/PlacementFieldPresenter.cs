using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Common.Services.Events;
using Game.Assets.Scripts.Game.Logic.Common.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Requests;
using Game.Assets.Scripts.Game.Logic.Presenters.Requests.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementFieldPresenter : BasePresenter<IPlacementFieldView>
    {
        private IPlacementFieldView _view;
        private readonly RequestLink<GetField> _fieldRequest;
        private readonly ICommands _commands;
        private readonly IEvents _events;
        private List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();
        private Subscriber<GhostStateChangedEvent> _onGhostChanged;
        private Subscriber<GhostPositionChangedEvent> _onGhostPositionChanged;
        private Subscriber<EntityAddedToRepositoryEvent<Construction>> _added;
        private Subscriber<EntityRemovedFromRepositoryEvent<Construction>> _removed;

        public PlacementFieldPresenter(IPlacementFieldView view) : this(view,
            IRequests.Default.Get<GetField>(),
            ICommands.Default,
            IEvents.Default)
        {
        }

        public PlacementFieldPresenter(IPlacementFieldView view, 
            RequestLink<GetField> fieldRequest,
            ICommands commands,
            IEvents events) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _fieldRequest = fieldRequest ?? throw new ArgumentNullException(nameof(fieldRequest));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
            _events = events ?? throw new ArgumentNullException(nameof(events));

            var boundaries = _fieldRequest.Get(new GetField()).Respond.Boudaries;
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    _cells.Add(CreateCell(new IntPoint(x, y)));
                }
            }

            _onGhostChanged = _events.Get<GhostStateChangedEvent>(HandleModeOnChanged);
            _onGhostPositionChanged = _events.Get<GhostPositionChangedEvent>(HandleOnPositionChanged);
            _added = _events.Get<EntityAddedToRepositoryEvent<Construction>>(HandleConstructionsOnAdded);
            _removed = _events.Get<EntityRemovedFromRepositoryEvent<Construction>>(HandleConstructionsOnRemoved);

            UpdateGhostCells();
        }

        protected override void DisposeInner()
        {
            _added.Dispose();
            _removed.Dispose();
            _onGhostChanged.Dispose();
            _onGhostPositionChanged.Dispose();
        }

        private PlacementCellPresenter CreateCell(IntPoint position)
        {
            var view = _view.CellsContainer.Spawn<ICellView>(_view.Cell);
            return new PlacementCellPresenter(view, position, _fieldRequest);
        }

        private void HandleOnPositionChanged(GhostPositionChangedEvent obj)
        {
            UpdateGhostCells();
        }

        private void HandleModeOnChanged(GhostStateChangedEvent obj)
        {
            UpdateGhostCells();
        }

        private void UpdateGhostCells()
        {
            var field = _fieldRequest.Get(new GetField()).Respond;
            foreach (var cell in _cells)
            {
                cell.SetState(field.Status[cell.Position]);
            }
        }

        private void HandleConstructionsOnRemoved(EntityRemovedFromRepositoryEvent<Construction> e)
        {
            UpdateGhostCells();
        }

        private void HandleConstructionsOnAdded(EntityAddedToRepositoryEvent<Construction> e)
        {
            _commands.Execute(new BuildConstructionCommand(e.Entity, _view.ConstrcutionContainer, _view.ConstrcutionPrototype));
            UpdateGhostCells();
        }
    }
}
