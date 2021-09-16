using Assets.Scripts.Game.Logic.Common.Math;

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
            return null;
            //return Scheme.GetOccupiedSpace(Position);
        }
    }
}
