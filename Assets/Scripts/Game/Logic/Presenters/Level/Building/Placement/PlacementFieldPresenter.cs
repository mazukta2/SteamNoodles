﻿using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions;
using Game.Assets.Scripts.Game.Logic.Aggregations.Fields;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Fields;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories.Fields;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementFieldPresenter : BasePresenter<IPlacementFieldView>
    {
        private readonly IPlacementFieldView _view;
        private readonly GhostPresentationRepository _ghost;
        private readonly FieldPresentation _field;
        private readonly ConstructionsPresentationRepository _constructions;
        private readonly List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(IPlacementFieldView view) : this(view,
            IPresenterServices.Default?.Get<GhostPresentationRepository>(),
            IPresenterServices.Default?.Get<FieldPresentationRepository>().Get(),
            IPresenterServices.Default?.Get<ConstructionsPresentationRepository>())
        {
        }

        public PlacementFieldPresenter(IPlacementFieldView view,
            GhostPresentationRepository ghost,
            FieldPresentation field,
            ConstructionsPresentationRepository constructions) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));

            foreach (var position in _field.AllCells.Cells)
                _cells.Add(CreateCell(position));

            //_ghost.OnEvent += HandleOnEvent;
            // _ghost.OnAdded += UpdateGhostCells;
            // _ghost.OnRemoved += UpdateGhostCells;

            // _field.OnEvent += HandleOnEvent;
            //
            // _constructions.OnAdded += HandleConstructionsOnAdded;
            // _constructions.OnRemoved += HandleConstructionsOnRemoved;

            UpdateGhostCells();
        }

        protected override void DisposeInner()
        {
            //_ghost.OnEvent -= HandleOnEvent;
            // _ghost.OnAdded -= UpdateGhostCells;
            // _ghost.OnRemoved -= UpdateGhostCells;
            // _field.OnEvent -= HandleOnEvent;
            //
            // _constructions.OnAdded -= HandleConstructionsOnAdded;
            // _constructions.OnRemoved -= HandleConstructionsOnRemoved;
        }

        private PlacementCellPresenter CreateCell(FieldPosition position)
        {
            var view = _view.CellsContainer.Spawn<ICellView>(_view.Cell);
            return new PlacementCellPresenter(view, position);
        }
        
        private void UpdateGhostCells()
        {
            IReadOnlyCollection<FieldPosition> freeCells = _field.AvailableCells.Cells;
            IReadOnlyCollection<FieldPosition> occupiedByGhostCells = null;
            // if (_ghost.Has())
            // {
            //     var ghost = _ghost.Get();
            //     occupiedByGhostCells = ghost.GetOccupiedSpace();
            // }

            foreach (var cell in _cells)
            {
                // var state = CellPlacementStatus.Normal;
                //
                // var isFreeCell = freeCells.Any(x => x.Value == cell.Position.Value);
                // if (_ghost.Has())
                // {
                //     // is under ghost
                //     if (occupiedByGhostCells.Any(x => x.Value == cell.Position.Value))
                //     {
                //         state = isFreeCell ? CellPlacementStatus.IsAvailableGhostPlace : CellPlacementStatus.IsNotAvailableGhostPlace;
                //     }
                //     else
                //     {
                //         if (isFreeCell)
                //             state = CellPlacementStatus.IsReadyToPlace;
                //         else
                //             state = CellPlacementStatus.IsUnderConstruction;
                //     }
                // }
                // else
                // {
                //     if (isFreeCell)
                //         state = CellPlacementStatus.Normal;
                //     else
                //         state = CellPlacementStatus.IsUnderConstruction;
                // }
                //
                // cell.SetState(state);
            }
        }

        private void HandleOnEvent(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent && obj is not FieldUpdateEvent)
                return;
            
            UpdateGhostCells();
        }
        
        private void HandleConstructionsOnRemoved(ConstructionPresentation construction)
        {
            UpdateGhostCells();
        }

        private void HandleConstructionsOnAdded(ConstructionPresentation construction)
        {
            _view.ConstrcutionContainer.Spawn<IConstructionView>(_view.ConstrcutionPrototype).Init(construction.Id);
            UpdateGhostCells();
        }
    }
}
