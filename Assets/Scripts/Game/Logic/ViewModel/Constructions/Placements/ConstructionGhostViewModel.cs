using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements
{
    public class ConstructionGhostViewModel
    {
        public ConstructionScheme Scheme { get; }
        private PlacementViewModel _placement;

        public ConstructionGhostViewModel(PlacementViewModel placement, ConstructionScheme scheme)
        {
            Scheme = scheme;
            _placement = placement;
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
    }
}
