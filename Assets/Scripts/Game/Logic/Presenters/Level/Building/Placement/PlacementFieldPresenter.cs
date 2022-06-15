using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementFieldPresenter : BasePresenter<IPlacementFieldView>
    {
        private IPlacementFieldView _view;
        private GhostService _ghostService;
        private readonly FieldService _fieldService;
        private readonly ConstructionsService _constructionsService;
        private List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(IPlacementFieldView view) : this(view,
            IPresenterServices.Default?.Get<GhostService>(),
            IPresenterServices.Default?.Get<FieldService>(),
            IPresenterServices.Default?.Get<ConstructionsService>())
        {
        }

        public PlacementFieldPresenter(IPlacementFieldView view,
            GhostService ghostService,
            FieldService fieldService,
            ConstructionsService constructionsService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));

            var boundaries = _fieldService.GetBoundaries();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    _cells.Add(CreateCell(new IntPoint(x, y)));
                }
            }

            _ghostService.OnChanged += HandleModeOnChanged;
            _ghostService.OnPositionChanged += HandleOnPositionChanged;

            _constructionsService.OnAdded += HandleConstructionsOnAdded;
            _constructionsService.OnRemoved += HandleConstructionsOnRemoved;

            UpdateGhostCells();
        }

        protected override void DisposeInner()
        {
            _ghostService.OnChanged -= HandleModeOnChanged;
            _ghostService.OnPositionChanged -= HandleOnPositionChanged;

            _constructionsService.OnAdded -= HandleConstructionsOnAdded;
            _constructionsService.OnRemoved -= HandleConstructionsOnRemoved;
        }

        private PlacementCellPresenter CreateCell(IntPoint position)
        {
            var view = _view.CellsContainer.Spawn<ICellView>(_view.Cell);
            return new PlacementCellPresenter(view, position, _fieldService);
        }

        private void HandleOnPositionChanged()
        {
            UpdateGhostCells();
        }

        private void HandleModeOnChanged(bool obj)
        {
            UpdateGhostCells();
        }

        private void UpdateGhostCells()
        {
            IReadOnlyCollection<FieldPosition> ocuppiedCells = null;
            var occupiedByBuildings = _constructionsService.GetAllOccupiedSpace();
            if (_ghostService.IsEnabled())
            {
                var scheme = _ghostService.GetCard().Scheme;
                ocuppiedCells = scheme.Placement
                    .GetOccupiedSpace(_ghostService.GetPosition(), _ghostService.GetRotation());
            }

            foreach (var cell in _cells)
            {
                var state = CellPlacementStatus.Normal;

                if (occupiedByBuildings.Any(x => x.Value == cell.Position))
                    state = CellPlacementStatus.IsUnderConstruction;

                if (_ghostService.IsEnabled())
                {
                    var scheme = _ghostService.GetCard().Scheme;
                    if (_constructionsService.IsFreeCell(scheme, new FieldPosition(cell.Position), _ghostService.GetRotation()))
                        state = CellPlacementStatus.IsReadyToPlace;

                    if (ocuppiedCells.Any(x => x.Value == cell.Position))
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
            //ConstructionShrinkerPresenter
            //_commands.Execute(new BuildConstructionCommand(construction, _view.ConstrcutionContainer, _view.ConstrcutionPrototype));
            UpdateGhostCells();
        }
    }
}
