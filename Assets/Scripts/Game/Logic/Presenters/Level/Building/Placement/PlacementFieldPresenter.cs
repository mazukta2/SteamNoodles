using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
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
        private PlacementField _model;
        private IPlacementFieldView _view;
        private GhostManagerPresenter _ghostManager;
        private IAssets _assets;
        private ConstructionsSettingsDefinition _settings;
        private List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(GhostManagerPresenter ghostManagerPresenter,
            PlacementField model,
            IPlacementFieldView view,
            ConstructionsSettingsDefinition settings, IAssets assets) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghostManager = ghostManagerPresenter ?? throw new ArgumentNullException(nameof(ghostManagerPresenter));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            for (int x = _model.Rect.xMin; x <= _model.Rect.xMax; x++)
            {
                for (int y = _model.Rect.yMin; y <= _model.Rect.yMax; y++)
                {
                    _cells.Add(CreateCell(new IntPoint(x, y)));
                }
            }

            _ghostManager.OnGhostChanged += UpdateGhostCells;
            _ghostManager.OnGhostPostionChanged += UpdateGhostCells;

            _model.OnConstructionAdded += HandleOnConstructionAdded;
            _model.OnConstructionRemoved += HandleOnConstructionRemoved;

            UpdateGhostCells();
        }

        protected override void DisposeInner()
        {
            _ghostManager.OnGhostChanged -= UpdateGhostCells;
            _ghostManager.OnGhostPostionChanged -= UpdateGhostCells;
            _model.OnConstructionAdded -= HandleOnConstructionAdded;
            _model.OnConstructionRemoved -= HandleOnConstructionRemoved;
        }

        private PlacementCellPresenter CreateCell(IntPoint position)
        {
            var view = _view.CellsContainer.Spawn<ICellView>(_view.Cell);
            return new PlacementCellPresenter(view, this, IGameDefinitions.Default.Get<ConstructionsSettingsDefinition>(), position);
        }

        public void UpdateGhostCells()
        {
            var ghost = _ghostManager.GetGhost();
            var ocuppiedCells = ghost != null ? ghost.Definition.GetOccupiedSpace(ghost.GetGridPosition(), ghost.Rotation) : null;
            var occupiedByBuildings = _model.GetAllOccupiedSpace();

            foreach (var cell in _cells)
            {
                var state = CellPlacementStatus.Normal;

                if (occupiedByBuildings.Any(x => x == cell.Position))
                    state = CellPlacementStatus.IsUnderConstruction;

                if (ghost != null)
                {
                    if (_model.IsFreeCell(ghost.Definition, cell.Position, ghost.Rotation))
                        state = CellPlacementStatus.IsReadyToPlace;

                    if (ocuppiedCells.Any(x => x == cell.Position))
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

        private void HandleOnConstructionAdded(Construction construction)
        {
            var view = _view.ConstrcutionContainer.Spawn<IConstructionView>(_view.ConstrcutionPrototype);
            new ConstructionPresenter(_settings, construction, IGameAssets.Default, view, _ghostManager, IGameControls.Default);
        }

        private void HandleOnConstructionRemoved(Construction obj)
        {
            UpdateGhostCells();
        }
    }
}
