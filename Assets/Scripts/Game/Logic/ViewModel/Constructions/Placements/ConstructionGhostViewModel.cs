using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements
{
    public class ConstructionGhostViewModel
    {
        public ConstructionScheme Scheme { get; }
        public IGhostConstructionView View { get; private set; }
        public Point Position { get; private set; }

        private PlacementViewModel _placement;
        

        public ConstructionGhostViewModel(PlacementViewModel placement, ConstructionScheme scheme, IGhostConstructionView view)
        {
            Scheme = scheme;
            _placement = placement;
            View = view;
            View.SetMoveAction(MoveTo);
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

        public void Destroy()
        {
            View.Destroy();
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
