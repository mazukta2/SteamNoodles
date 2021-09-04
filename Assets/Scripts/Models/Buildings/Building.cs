using Assets.Scripts.Data.Buildings;
using System;
using UnityEngine;

namespace Assets.Scripts.Models.Buildings
{
    public class Building
    {
        public Vector2Int Position { get; private set; }

        private BuildingsGrid _grid;

        public BuildingSchemeData Scheme { get; private set; }

        public Building(BuildingsGrid grid, BuildingScheme scheme, Vector2Int position)
        {
            Scheme = scheme.GetData();
            Position = position;
            _grid = grid;
        }

        public Vector3 GetWorldPosition()
        {
            return _grid.GetWorldPosition(Position);
        }
    }
}
