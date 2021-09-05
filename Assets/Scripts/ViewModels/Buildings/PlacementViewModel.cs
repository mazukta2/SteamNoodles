using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;
using Assets.Scripts.Models.Levels;
using Assets.Scripts.Views.Cameras;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ViewModels.Buildings
{
    public class PlacementViewModel : Placement
    {
        public BuildingSchemeViewModel Ghost { get; private set; }

        public PlacementViewModel(Placement placement) : base(placement)
        {
        }

        public void SetGhost(BuildingSchemeViewModel buildingScheme)
        {
            if (buildingScheme == null) throw new ArgumentNullException(nameof(buildingScheme));
            if (Ghost != null) throw new Exception("Ghost is not null");

            Ghost = buildingScheme;
            History.Add(new GhostSetEvent(buildingScheme));
        }

        public void ClearGhost()
        {
            if (Ghost == null) throw new Exception("Ghost is null");

            Ghost = null;
            History.Add(new GhostClearEvent());
        }

        public Vector2Int GetGhostCell()
        {
            var mouseWorldPosition = MainCameraController.Instance.ScreenToWorld(Input.mousePosition);
            var offset = new Vector3(Ghost.Size.x * CellSize / 2 - CellSize / 2,
                Ghost.Size.y * CellSize / 2 - CellSize / 2);
            mouseWorldPosition -= offset;

            var mousePosX = Mathf.RoundToInt(mouseWorldPosition.x / CellSize);
            var mousePosY = Mathf.RoundToInt(mouseWorldPosition.y / CellSize);

            var cell = new Vector2Int(Mathf.CeilToInt(mousePosX), Mathf.CeilToInt(mousePosY));
            return cell;
        }

        public Vector3 GetGhostPosition()
        {
            return GetWorldPosition(GetGhostCell());
        }

        public void BuildGhost()
        {
            Place(Ghost, GetGhostCell());
        }

        new public Building Place(BuildingScheme scheme, Vector2Int position)
        {
            var building = base.Place(scheme, position);
            //History.Add(new BuildingAddedEvent(new BuildingViewModel(building)));
            return building;
        }

        public class GhostSetEvent : IGameEvent
        {
            public BuildingSchemeViewModel BuildingScheme { get; set; }
            public GhostSetEvent(BuildingSchemeViewModel buildingScheme) => BuildingScheme = buildingScheme;
        }

        public class GhostClearEvent : IGameEvent
        {

        }

        new public class BuildingAddedEvent : IGameEvent
        {
            public BuildingAddedEvent(BuildingViewModel building)
            {
                Building = building;
            }

            public BuildingViewModel Building { get; set; }
        }
    }
}
