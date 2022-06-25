using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Fields;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementFieldPresenter : BasePresenter<IPlacementFieldView>
    {
        private readonly IPlacementFieldView _view;
        private readonly ISingleQuery<ConstructionGhost> _ghost;
        private readonly ISingleQuery<Field> _field;
        private readonly IQuery<Construction> _constructions;
        private readonly List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(IPlacementFieldView view) : this(view,
            IPresenterServices.Default?.Get<ISingletonRepository<ConstructionGhost>>().AsQuery(),
            IPresenterServices.Default?.Get<ISingletonRepository<Field>>()?.AsQuery(),
            IPresenterServices.Default?.Get<IRepository<Construction>>().AsQuery())
        {
        }

        public PlacementFieldPresenter(IPlacementFieldView view,
            ISingleQuery<ConstructionGhost> ghost,
            ISingleQuery<Field> field,
            IQuery<Construction> constructions) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));

            var boundaries = _field.Get().GetBoundaries();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    _cells.Add(CreateCell(new FieldPosition(_field.Get(), x, y)));
                }
            }

            _ghost.OnEvent += HandleOnEvent;
            _ghost.OnAdded += UpdateGhostCells;
            _ghost.OnRemoved += UpdateGhostCells;

            _field.OnEvent += HandleOnEvent;

            _constructions.OnAdded += HandleConstructionsOnAdded;
            _constructions.OnRemoved += HandleConstructionsOnRemoved;

            UpdateGhostCells();
        }

        protected override void DisposeInner()
        {
            _field.Dispose();
            _constructions.Dispose();
            _ghost.Dispose();
            _ghost.OnEvent -= HandleOnEvent;
            _ghost.OnAdded -= UpdateGhostCells;
            _ghost.OnRemoved -= UpdateGhostCells;
            _field.OnEvent -= HandleOnEvent;

            _constructions.OnAdded -= HandleConstructionsOnAdded;
            _constructions.OnRemoved -= HandleConstructionsOnRemoved;
        }

        private PlacementCellPresenter CreateCell(FieldPosition position)
        {
            var view = _view.CellsContainer.Spawn<ICellView>(_view.Cell);
            return new PlacementCellPresenter(view, position);
        }
        
        private void UpdateGhostCells()
        {
            IReadOnlyCollection<FieldPosition> freeCells = _field.Get().AvailableCells.Cells;
            IReadOnlyCollection<FieldPosition> occupiedByGhostCells = null;
            ConstructionGhost ghost = null;
            if (_ghost.Has())
            {
                ghost = _ghost.Get();
                occupiedByGhostCells = ghost.Card.Scheme.Placement.GetOccupiedSpace(ghost.Position, ghost.Rotation);
            }

            foreach (var cell in _cells)
            {
                var state = CellPlacementStatus.Normal;

                var isFreeCell = freeCells.Any(x => x.Value == cell.Position.Value);
                if (ghost != null)
                {
                    // is under ghost
                    if (occupiedByGhostCells.Any(x => x.Value == cell.Position.Value))
                    {
                        state = isFreeCell ? CellPlacementStatus.IsAvailableGhostPlace : CellPlacementStatus.IsNotAvailableGhostPlace;
                    }
                    else
                    {
                        if (isFreeCell)
                            state = CellPlacementStatus.IsReadyToPlace;
                        else
                            state = CellPlacementStatus.IsUnderConstruction;
                    }
                }
                else
                {
                    if (isFreeCell)
                        state = CellPlacementStatus.Normal;
                    else
                        state = CellPlacementStatus.IsUnderConstruction;
                }

                cell.SetState(state);
            }
        }

        private void HandleOnEvent(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent && obj is not FieldUpdateEvent)
                return;
            
            UpdateGhostCells();
        }
        
        private void HandleConstructionsOnRemoved(Construction construction)
        {
            UpdateGhostCells();
        }

        private void HandleConstructionsOnAdded(Construction construction)
        {
            _view.ConstrcutionContainer.Spawn<IConstructionView>(_view.ConstrcutionPrototype).Init(
                _constructions.GetAsQuery(construction.Id));
            UpdateGhostCells();
        }
    }
}
