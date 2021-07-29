using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Models.Events;
using Assets.Scripts.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Models.Buildings
{
    public class SessionBuildings 
    {

        public readonly float CellSize = 1;

        private List<BuildingScheme> _schemes = new List<BuildingScheme>();
        private List<Building> _buildings = new List<Building>();
        private BuildingsData _buildingData;

        public SessionBuildings(BuildingsData buildings)
        {
            _buildingData = buildings;

            foreach (var item in _buildingData.Buildings)
            {
                _schemes.Add(new BuildingScheme(item));
            }
        }

        public BuildingScheme[] GetCurrentSchemes() => _schemes.ToArray();
        public Building[] GetBuildings() => _buildings.ToArray();

        public bool CanBuild(BuildingScheme scheme, Vector2Int position)
        {
            if (!IsInside(scheme, position))
                return false;

            var ocupiedPlace = _buildings.SelectMany(x => x.GetOccupiedSpace());
            foreach (var pos in scheme.GetOccupiedSpace(position))
            {
                if (ocupiedPlace.Any(x => x == pos))
                    return false;
            }

            return true;
        }

        public BuildingPlacedEvent Build(BuildingScheme scheme, Vector2Int position)
        {
            if (!CanBuild(scheme, position))
                throw new Exception("Can't build");

            var building = new Building(scheme, position);
            _buildings.Add(building);

            return new BuildingPlacedEvent(building);
        }

        public bool IsInside(BuildingScheme scheme, Vector2Int position)
        {
            var rect = new RectInt(-_buildingData.MapSize.x, -_buildingData.MapSize.y, _buildingData.MapSize.x * 2, _buildingData.MapSize.y * 2);
            foreach (var pos in scheme.GetOccupiedSpace(position))
            {
                if (!rect.Contains(pos))
                    return false;
            }

            return true;
        }

        public Vector3 GetWorldPosition(Vector2Int cell)
        {
            return new Vector3(cell.x * CellSize - CellSize / 2, cell.y * CellSize - CellSize);
        }
    }
}
