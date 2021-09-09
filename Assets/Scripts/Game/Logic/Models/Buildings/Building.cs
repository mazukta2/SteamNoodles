using Assets.Scripts.Game.Logic.Common.Math;

namespace Assets.Scripts.Models.Buildings
{
    public class Building
    {
        private Placement _grid;

        public Building(Placement grid, BuildingScheme scheme, Point position)
        {
            Scheme = scheme;
            Position = position;
            _grid = grid;
        }

        public Building(Building origin)
        {
            Scheme = origin.Scheme;
            Position = origin.Position;
            _grid = origin._grid;
        }

        public Point Position { get; private set; }
        public BuildingScheme Scheme { get; private set; }

        //public Vector3 GetWorldPosition()
        //{
        //    return _grid.GetWorldPosition(Position);
        //}

        public Point[] GetOccupiedScace()
        {
            return null;
            //return Scheme.GetOccupiedSpace(Position);
        }
    }
}
