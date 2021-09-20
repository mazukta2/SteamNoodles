using Assets.Scripts.Game.Logic.Common.Math;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Assets.Scripts.Models.Buildings
{
    public class Construction
    {
        private Placement _grid;

        public Construction(Placement grid, ConstructionScheme scheme, Point position)
        {
            Scheme = scheme;
            Position = position;
            _grid = grid;
        }

        public Construction(Construction origin)
        {
            Scheme = origin.Scheme;
            Position = origin.Position;
            _grid = origin._grid;
        }

        public Point Position { get; private set; }
        public ConstructionScheme Scheme { get; private set; }

        public Point[] GetOccupiedScace()
        {
            return Scheme.GetOccupiedSpace(Position);
        }

        public IVisual GetVisual()
        {
            return Scheme.BuildingView;
        }
    }
}
