using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;
using Assets.Scripts.Models.Levels;
using Assets.Scripts.Views.Cameras;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingViewModel
    {
        public History History { get; } = new History();
        public BuildingScheme Ghost { get; private set; }

        private GameLevel _level;

        public BuildingViewModel(GameLevel level)
        {
            _level = level;
        }

        public void SetGhost(BuildingScheme buildingScheme)
        {
            Ghost = buildingScheme;
            History.Add(new GhostSetEvent(buildingScheme));
        }

        public void ClearGhost()
        {
            Ghost = null;
            History.Add(new GhostClearEvent());
        }

        public BuildingsGrid GetGrid()
        {
            return _level.GetGrid();
        }

        public BuildingsPool GetPool()
        {
            return _level.GetBuildingPool();
        }

        public class GhostSetEvent : IGameEvent
        {
            public BuildingScheme BuildingScheme { get; set; }
            public GhostSetEvent(BuildingScheme buildingScheme)
            {
                BuildingScheme = buildingScheme;
            }
        }

        public class GhostClearEvent : IGameEvent
        {

        }

        public Vector2Int GetGhostCell()
        {
            var grid = GetGrid();
            var mouseWorldPosition = MainCameraController.Instance.ScreenToWorld(Input.mousePosition);
            var offset = new Vector3(Ghost.Size.x * grid.CellSize / 2 - grid.CellSize/2, Ghost.Size.y * grid.CellSize / 2 - grid.CellSize / 2);
            mouseWorldPosition -= offset;

            var mousePosX = Mathf.RoundToInt(mouseWorldPosition.x / grid.CellSize);
            var mousePosY = Mathf.RoundToInt(mouseWorldPosition.y / grid.CellSize);

            var cell = new Vector2Int(Mathf.CeilToInt(mousePosX), Mathf.CeilToInt(mousePosY));
            return cell;
        }

        public Vector3 GetGhostPosition()
        {
            var grid = GetGrid();
            return grid.GetWorldPosition(GetGhostCell());
        }

        public void BuildGhost()
        {
            GetGrid().Place(Ghost, GetGhostCell());
        }
    }
}
