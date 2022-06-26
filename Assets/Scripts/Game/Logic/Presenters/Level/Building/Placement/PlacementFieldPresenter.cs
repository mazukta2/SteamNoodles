using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.DataObjects.Fields;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Fields;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementFieldPresenter : BasePresenter<IPlacementFieldView>
    {
        private readonly IPlacementFieldView _view;
        private readonly IDataProvider<GhostData> _ghost;
        private readonly IDataProvider<FieldData> _field;
        private readonly IDataCollectionProvider<ConstructionData> _constructions;
        private readonly List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(IPlacementFieldView view) : this(view,
            IPresenterServices.Default?.Get<IDataProviderService<GhostData>>().Get(),
            IPresenterServices.Default?.Get<IDataProviderService<FieldData>>().Get(),
            IPresenterServices.Default?.Get<IDataCollectionProviderService<ConstructionData>>().Get())
        {
        }

        public PlacementFieldPresenter(IPlacementFieldView view,
            IDataProvider<GhostData> ghost,
            IDataProvider<FieldData> field,
            IDataCollectionProvider<ConstructionData> constructions) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));

            foreach (var position in _field.Get().AllCells.Cells)
                _cells.Add(CreateCell(position));

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
            if (_ghost.Has())
            {
                var ghost = _ghost.Get();
                occupiedByGhostCells = ghost.Card.Scheme.Placement.GetOccupiedSpace(ghost.Position, ghost.Rotation);
            }

            foreach (var cell in _cells)
            {
                var state = CellPlacementStatus.Normal;

                var isFreeCell = freeCells.Any(x => x.Value == cell.Position.Value);
                if (_ghost.Has())
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
        
        private void HandleConstructionsOnRemoved(IDataProvider<ConstructionData> construction)
        {
            UpdateGhostCells();
        }

        private void HandleConstructionsOnAdded(IDataProvider<ConstructionData> construction)
        {
            _view.ConstrcutionContainer.Spawn<IConstructionView>(_view.ConstrcutionPrototype).Init(construction);
            UpdateGhostCells();
        }
    }
}
