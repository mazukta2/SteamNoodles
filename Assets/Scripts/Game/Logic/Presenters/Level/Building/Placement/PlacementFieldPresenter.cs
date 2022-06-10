using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Common.Services.Events;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
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
        private readonly FieldModel _field;
        private readonly ICommands _commands;
        private readonly IEvents _events;
        private List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();
        private Subscriber<GhostStateChangedEvent> _onGhostChanged;
        private Subscriber<GhostPositionChangedEvent> _onGhostPositionChanged;
        private Subscriber<EntityAddedToRepositoryEvent<Construction>> _added;
        private Subscriber<EntityRemovedFromRepositoryEvent<Construction>> _removed;

        public PlacementFieldPresenter(IPlacementFieldView view) : this(view,
            IPresenterServices.Default.Get<FieldRequestsService>().Get(),
            ICommands.Default,
            IEvents.Default)
        {
        }

        public PlacementFieldPresenter(IPlacementFieldView view,
            FieldModel field,
            ICommands commands,
            IEvents events) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
            _events = events ?? throw new ArgumentNullException(nameof(events));

            var boundaries = _field.Boudaries;
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
            _field.Dispose();
            _added.Dispose();
            _removed.Dispose();
            _onGhostChanged.Dispose();
            _onGhostPositionChanged.Dispose();
        }

        private PlacementCellPresenter CreateCell(IntPoint position)
        {
            var view = _view.CellsContainer.Spawn<ICellView>(_view.Cell);
            return new PlacementCellPresenter(view, position, _field);
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
            foreach (var cell in _cells)
            {
                cell.SetState(_field.Status[cell.Position]);
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
