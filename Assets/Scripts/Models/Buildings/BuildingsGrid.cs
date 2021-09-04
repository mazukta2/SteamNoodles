using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models.Buildings
{
    public class BuildingsGrid
    {
        public readonly float CellSize = 0.25f;
        public Vector2Int Size { get; }
        public List<Building> Buildings { get; set; } = new List<Building>();
        public History History { get; } = new History();

        public BuildingsGrid(BuildingsData data)
        {
            Size = data.MapSize;
        }

        public RectInt GetRect()
        {
            return new RectInt(-Size.x, -Size.y, Size.x, Size.y);
        }

        public Vector3 GetWorldPosition(Vector2Int cell)
        {
            return new Vector3(cell.x * CellSize - CellSize / 2, cell.y * CellSize - CellSize / 2);
        }

        public class BuildingAddedEvent : IGameEvent
        {
            public BuildingAddedEvent(Building building)
            {
                Building = building;
            }

            public Building Building { get; set; }
        }

        public void Place(BuildingScheme scheme, Vector2Int position)
        {
            var building = new Building(this, scheme, position);
            Buildings.Add(building);
            History.Add(new BuildingAddedEvent(building));
        }
    }
}
