using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementFieldPresenter : BasePresenter<IPlacementFieldView>
    {
        private readonly IPlacementFieldView _view;
        private readonly GhostService _ghostService;
        private readonly Field _field;
        private readonly IRepository<Construction> _constructions;
        private readonly List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(IPlacementFieldView view) : this(view,
            IPresenterServices.Default?.Get<GhostService>(),
            IPresenterServices.Default?.Get<ISingletonRepository<Field>>()?.Get(),
            IPresenterServices.Default?.Get<IRepository<Construction>>())
        {
        }

        public PlacementFieldPresenter(IPlacementFieldView view,
            GhostService ghostService,
            Field field,
            IRepository<Construction> constructions) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));

            var boundaries = _field.GetBoundaries();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    _cells.Add(CreateCell(new FieldPosition(_field, x, y)));
                }
            }

            _ghostService.OnChanged += UpdateGhostCells;
            _ghostService.OnShowed += UpdateGhostCells;
            _ghostService.OnHided += UpdateGhostCells;

            _constructions.OnAdded += HandleConstructionsOnAdded;
            _constructions.OnRemoved += HandleConstructionsOnRemoved;

            UpdateGhostCells();
        }

        protected override void DisposeInner()
        {
            _ghostService.OnChanged -= UpdateGhostCells;
            _ghostService.OnShowed -= UpdateGhostCells;
            _ghostService.OnHided -= UpdateGhostCells;

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
            var freeCells = _constructions.GetUnoccupiedCells(_field);
            
            IReadOnlyCollection<FieldPosition> occupiedByGhostCells = null;
            if (_ghostService.IsEnabled())
            {
                var ghost = _ghostService.GetGhost();
                var scheme = ghost.Card.Scheme;
                occupiedByGhostCells = scheme.Placement.GetOccupiedSpace(ghost.Position, ghost.Rotation);
            }

            foreach (var cell in _cells)
            {
                var state = CellPlacementStatus.Normal;

                var isFreeCell = freeCells.Any(x => x.Value == cell.Position.Value);

                if (!isFreeCell)
                    state = CellPlacementStatus.IsUnderConstruction;

                if (occupiedByGhostCells != null)
                {
                    var ghost = _ghostService.GetGhost();
                    var scheme = ghost.Card.Scheme;
                    
                    // is available
                    var isAvailable = ConstructionsFunctions.IsAvailableToBuild(scheme, cell.Position, ghost.Rotation);
                    
                    if (isFreeCell && isAvailable)
                        state = CellPlacementStatus.IsReadyToPlace;

                    // is under ghost
                    if (occupiedByGhostCells.Any(x => x.Value == cell.Position.Value))
                    {
                        if (state == CellPlacementStatus.IsReadyToPlace)
                            state = CellPlacementStatus.IsAvailableGhostPlace;
                        else
                            state = CellPlacementStatus.IsNotAvailableGhostPlace;
                    }
                }

                cell.SetState(state);
            }
        }

        private void HandleConstructionsOnRemoved(Construction construction)
        {
            UpdateGhostCells();
        }

        private void HandleConstructionsOnAdded(Construction construction)
        {
            _view.ConstrcutionContainer.Spawn<IConstructionView>(_view.ConstrcutionPrototype).Init(_constructions.GetAsQuery(construction.Id));
            UpdateGhostCells();
        }
    }
}
