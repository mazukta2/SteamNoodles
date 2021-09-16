using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.Models.Events.GameEvents;
using Tests.Assets.Scripts.Game.Logic.Views;
using static Assets.Scripts.Models.Buildings.Placement;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements
{
    public class PlacementViewModel
    {
        public readonly float CellSize = 0.25f;

        private Placement _model;
        private HistoryReader _historyReader;
        private List<ConstructionViewModel> _constructions = new List<ConstructionViewModel>();
        private List<CellViewModel> _cells = new List<CellViewModel>();

        public PlacementViewModel(Placement model, IPlacementView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            View = view;
            View.SetClick(OnClick);

            for (int x = _model.Rect.xMin; x <= _model.Rect.xMax; x++)
            {
                for (int y = _model.Rect.yMin; y <= _model.Rect.yMax; y++)
                {
                    _cells.Add(new CellViewModel(this, new Point(x, y), View.CreateCell()));
                }
            }

            _historyReader = new HistoryReader(model.History);
            _historyReader.Subscribe<ConstrcutionAddedEvent>(OnConstruction).Update();
        }

        public bool CanPlace(ConstructionScheme scheme, Point position)
        {
            return _model.CanPlace(scheme, position);
        }

        public Point GetSize() => _model.Size;

        public ConstructionGhostViewModel Ghost { get; private set; }
        public IPlacementView View { get; private set; }

        public void SetGhost(ConstructionScheme obj)
        {
            if (Ghost != null) throw new Exception("Ghost already existing");

            Ghost = new ConstructionGhostViewModel(this, obj, View.CreateGhost());
            UpdateGhostCells();
        }

        public void ClearGhost()
        {
            Ghost.Destroy();
            Ghost = null;
            UpdateGhostCells();
        }

        public void OnClick(Vector2 worldPosition)
        {
            if (Ghost != null)
            {
                if (Ghost.CanPlaceGhost())
                {
                    _model.Place(Ghost.Scheme, Ghost.GetCellPosition(worldPosition));
                    _historyReader.Update();
                    ClearGhost();
                }
            }

        }

        public ConstructionViewModel[] GetConstructions()
        {
            return _constructions.ToArray();
        }

        public CellViewModel[] GetCells()
        {
            return _cells.ToArray();
        }

        public void UpdateGhostCells()
        {
            foreach (var cell in _cells)
            {
                var state = CellViewModel.CellState.Normal;
                if (Ghost != null)
                {
                    var ocuppiedCells = Ghost.Scheme.GetOccupiedSpace(Ghost.Position);
                    if (_model.IsFreeCell(Ghost.Scheme, cell.Position))
                        state = CellViewModel.CellState.IsReadyToPlace;

                    if (ocuppiedCells.Any(x => x == cell.Position))
                    {
                        state = CellViewModel.CellState.IsAvailableGhostPlace;
                    }
                }

                cell.SetState(state);
            }
        }

        public Vector2 GetWorldPosition(Point point)
        {
            return new Vector2(point.X * CellSize, point.Y * CellSize);
        }

        private void OnConstruction(ConstrcutionAddedEvent obj)
        {
            _constructions.Add(new ConstructionViewModel(this, obj.Construction, View.CreateConstrcution()));
        }


        //public IPoint GetWorldPosition(IPoint cell)
        //{
        //    return new Vector3(cell.x * CellSize - CellSize / 2, cell.y * CellSize - CellSize / 2);
        //}

        //public Point WorldToCell(Vector2 positon)
        //{
        //    var offset = new Vector3(Size.X * CellSize / 2 - CellSize / 2,
        //        Ghost.Size.y * CellSize / 2 - CellSize / 2);

        //    mouseWorldPosition -= offset;

        //    var mousePosX = Mathf.RoundToInt(mouseWorldPosition.x / CellSize);
        //    var mousePosY = Mathf.RoundToInt(mouseWorldPosition.y / CellSize);

        //    var cell = new Vector2Int(Mathf.CeilToInt(mousePosX), Mathf.CeilToInt(mousePosY));
        //    return cell;
        //}
    }
}
