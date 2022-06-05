using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementFieldPresenter : BasePresenter<IPlacementFieldView>
    {
        private IPlacementFieldView _view;
        private BuildingModeService _buildingModeService;
        private readonly IPresenterRepository<Construction> _constructions;
        private readonly FieldService _fieldService;
        private readonly ConstructionsService _constructionsService;
        private readonly IPresenterCommands _commands;
        private List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(IPlacementFieldView view) : this(view,
            IPresenterServices.Default?.Get<BuildingModeService>(),
            IPresenterServices.Default?.Get<FieldService>(),
            IPresenterServices.Default?.Get<ConstructionsService>(),
            IStageLevelPresenterRepositories.Default.Constructions,
            IPresenterCommands.Default)
        {
        }

        public PlacementFieldPresenter(IPlacementFieldView view, 
            BuildingModeService buildingModeService,
            FieldService fieldService,
            ConstructionsService constructionsService,
            IPresenterRepository<Construction> constructions,
            IPresenterCommands commands) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));

            var boundaries = _fieldService.GetBoundaries();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    _cells.Add(CreateCell(new IntPoint(x, y)));
                }
            }

            _buildingModeService.OnChanged += HandleModeOnChanged;
            _buildingModeService.OnPositionChanged += HandleOnPositionChanged;

            _constructions.OnAdded += HandleConstructionsOnAdded;
            _constructions.OnRemoved += HandleConstructionsOnRemoved;

            UpdateGhostCells();
        }

        protected override void DisposeInner()
        {
            _buildingModeService.OnChanged -= HandleModeOnChanged;
            _buildingModeService.OnPositionChanged -= HandleOnPositionChanged;

            _constructions.OnAdded -= HandleConstructionsOnAdded;
            _constructions.OnRemoved -= HandleConstructionsOnRemoved;
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
            if (_buildingModeService.IsEnabled)
            {
                var scheme = _buildingModeService.Card.Scheme;
                ocuppiedCells = scheme.Placement
                    .GetOccupiedSpace(_buildingModeService.GetPosition(), _buildingModeService.GetRotation());
            }

            foreach (var cell in _cells)
            {
                var state = CellPlacementStatus.Normal;

                if (occupiedByBuildings.Any(x => x.Value == cell.Position))
                    state = CellPlacementStatus.IsUnderConstruction;

                if (_buildingModeService.IsEnabled)
                {
                    var scheme = _buildingModeService.Card.Scheme;
                    if (_constructionsService.IsFreeCell(scheme, new FieldPosition(cell.Position), _buildingModeService.GetRotation()))
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

        private void HandleConstructionsOnRemoved(EntityLink<Construction> link, Construction construction)
        {
            UpdateGhostCells();
        }

        private void HandleConstructionsOnAdded(EntityLink<Construction> link, Construction construction)
        {
            _commands.Execute(new BuildConstructionCommand(construction, _view.ConstrcutionContainer, _view.ConstrcutionPrototype));
            UpdateGhostCells();
        }
    }
}
