using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using System;
using System.Numerics;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionGhostPresenter : Disposable
    {
        public ConstructionCard Scheme { get; }
        public IGhostConstructionView View { get; private set; }
        public Point Position { get; private set; }

        private PlacementPresenter _placement;


        public ConstructionGhostPresenter(PlacementPresenter placement, ConstructionCard scheme, IGhostConstructionView view)
        {
            Scheme = scheme;
            _placement = placement;
            View = view;
            View.SetMoveAction(MoveTo);
            View.SetImage(Scheme.BuildingView);
        }


        protected override void DisposeInner()
        {
            View.Dispose();
        }

        public Point GetCellPosition(Vector2 worldPosition)
        {
            var halfCell = _placement.CellSize / 2;

            var offset = new Vector2(Scheme.CellSize.X * halfCell - halfCell,
                Scheme.CellSize.Y * halfCell - halfCell);

            var pos = worldPosition - offset;

            var mousePosX = Math.Round(pos.X / _placement.CellSize);
            var mousePosY = Math.Round(pos.Y / _placement.CellSize);

            return new Point((int)Math.Ceiling(mousePosX), (int)Math.Ceiling(mousePosY));
        }

        public bool CanPlaceGhost()
        {
            return _placement.CanPlace(Scheme, Position);
        }


        public void MoveTo(Vector2 worldPosition)
        {
            Position = GetCellPosition(worldPosition);
            View.PlaceTo(_placement.GetWorldPosition(Position));
            View.SetCanBePlacedState(CanPlaceGhost());
            _placement.UpdateGhostCells();
        }
    }
}
