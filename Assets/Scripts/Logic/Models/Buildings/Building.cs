using UnityEngine;

namespace Assets.Scripts.Models.Buildings
{
    public class Building
    {
        private Placement _grid;

        public Building(Placement grid, BuildingScheme scheme, Vector2Int position)
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

        public Vector2Int Position { get; private set; }
        public BuildingScheme Scheme { get; private set; }

        public Vector3 GetWorldPosition()
        {
            return _grid.GetWorldPosition(Position);
        }

        public Vector2Int[] GetOccupiedScace()
        {
            return null;
            //return Scheme.GetOccupiedSpace(Position);
        }
    }
}
