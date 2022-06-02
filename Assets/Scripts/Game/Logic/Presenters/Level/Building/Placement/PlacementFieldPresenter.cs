using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
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
        //private GhostManagerPresenter _ghostManager;
        private readonly IPresenterRepository<Construction> _constructions;
        private readonly FieldService _fieldService;
        private readonly ConstructionsService _constructionsService;
        private IAssets _assets;
        private ConstructionsSettingsDefinition _settings;
        private List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(IPlacementFieldView view) : this(view,
            IPresenterServices.Default?.Get<BuildingModeService>(),
            IPresenterServices.Default?.Get<FieldService>(),
            IPresenterServices.Default?.Get<ConstructionsService>())
        {
            //IGhostManagerView.Default.Presenter,
            //    IStageLevelPresenterRepository.Default.Constructions,
            //    IStageLevelService.Default.Field,
            //    IStageLevelService.Default.Building,
            //    this,
            //    IGameDefinitions.Default.Get<ConstructionsSettingsDefinition>(), IGameAssets.Default
        }

        public PlacementFieldPresenter(IPlacementFieldView view, 
            BuildingModeService buildingModeService,
            FieldService fieldService,
            ConstructionsService constructionsService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));

            //_ghostManager = ghostManagerPresenter ?? throw new ArgumentNullException(nameof(ghostManagerPresenter));
            //_constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            //_buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
            //_assets = assets ?? throw new ArgumentNullException(nameof(assets));
            //_settings = settings ?? throw new ArgumentNullException(nameof(settings));

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

            //_ghostManager.OnGhostChanged += UpdateGhostCells;
            //_ghostManager.OnGhostPostionChanged += UpdateGhostCells;

            //_constructions.OnAdded += HandleOnConstructionAdded;
            //_constructions.OnRemoved += HandleOnConstructionRemoved;

            UpdateGhostCells();
        }

        //public PlacementFieldPresenter(GhostManagerPresenter ghostManagerPresenter,
        //    IPresenterRepository<Construction> constructions,
        //    IFieldPresenterService field,
        //    IBuildingPresenterService buildingService,
        //    IPlacementFieldView view,
        //    ConstructionsSettingsDefinition settings, IAssets assets) : base(view)
        //{
        //    _view = view ?? throw new ArgumentNullException(nameof(view));
        //    _ghostManager = ghostManagerPresenter ?? throw new ArgumentNullException(nameof(ghostManagerPresenter));
        //    _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
        //    _field = field ?? throw new ArgumentNullException(nameof(field));
        //    _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
        //    _assets = assets ?? throw new ArgumentNullException(nameof(assets));
        //    _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        //    var boundaries = field.GetBoundaries();
        //    for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
        //    {
        //        for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
        //        {
        //            _cells.Add(CreateCell(new IntPoint(x, y)));
        //        }
        //    }

        //    _ghostManager.OnGhostChanged += UpdateGhostCells;
        //    _ghostManager.OnGhostPostionChanged += UpdateGhostCells;

        //    _constructions.OnAdded += HandleOnConstructionAdded;
        //    _constructions.OnRemoved += HandleOnConstructionRemoved;

        //    UpdateGhostCells();
        //}

        protected override void DisposeInner()
        {
            _buildingModeService.OnChanged -= HandleModeOnChanged;
            _buildingModeService.OnPositionChanged -= HandleOnPositionChanged;
            //_ghostManager.OnGhostChanged -= UpdateGhostCells;
            //_ghostManager.OnGhostPostionChanged -= UpdateGhostCells;
            //_constructions.OnAdded -= HandleOnConstructionAdded;
            //_constructions.OnRemoved -= HandleOnConstructionRemoved;
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

        //private void HandleOnConstructionAdded(EntityLink<Construction> arg1, Construction arg2)
        //{
        //    //var view = _view.ConstrcutionContainer.Spawn<IConstructionView>(_view.ConstrcutionPrototype);
        //    //new ConstructionPresenter(_settings, arg1, _field, IGameAssets.Default, view, _ghostManager, IGameControls.Default);
        //}

        //private void HandleOnConstructionRemoved(EntityLink<Construction> arg1, Construction arg2)
        //{
        //    UpdateGhostCells();
        //}
    }
}
