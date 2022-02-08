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
        public ConstructionCard Card { get; }
        public IGhostConstructionView View { get; private set; }
        public Point Position { get; private set; }

        private PlacementPresenter _placement;


        public ConstructionGhostPresenter(PlacementPresenter placement, ConstructionCard originCard, IGhostConstructionView view)
        {
            Card = originCard;
            _placement = placement;
            View = view;
            View.SetMoveAction(MoveTo);
            View.SetImage(Card.BuildingView);
        }


        protected override void DisposeInner()
        {
            View.Dispose();
        }

        public Point GetCellPosition(FloatPoint worldPosition)
        {
            var halfCell = _placement.CellSize / 2;

            var offset = new FloatPoint(Card.CellSize.X * halfCell - halfCell,
                Card.CellSize.Y * halfCell - halfCell);

            var pos = worldPosition - offset;

            var mousePosX = Math.Round(pos.X / _placement.CellSize);
            var mousePosY = Math.Round(pos.Y / _placement.CellSize);

            return new Point((int)Math.Ceiling(mousePosX), (int)Math.Ceiling(mousePosY));
        }

        public bool CanPlaceGhost()
        {
            return _placement.CanPlace(Card, Position);
        }


        public void MoveTo(FloatPoint worldPosition)
        {
            Position = GetCellPosition(worldPosition);
            View.PlaceTo(_placement.GetWorldPosition(Position));
            View.SetCanBePlacedState(CanPlaceGhost());
            _placement.UpdateGhostCells();
        }
    }
}
