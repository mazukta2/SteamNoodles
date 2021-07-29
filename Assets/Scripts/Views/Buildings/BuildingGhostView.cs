using Assets.Scripts.Core;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Requests;
using Assets.Scripts.Views.Cameras;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingGhostView : GameMonoBehaviour, IBuildingsRequest
    {
        public BuildingScheme Scheme { get; private set; }
        public SessionBuildings Buildings { get; set; }

        private GameObject _ghost;
        private GameInputs _inputs = new GameInputs();

        protected void Start()
        {
            Messages.Publish<IBuildingsRequest>(this);
        }

        public void SetGhost(BuildingScheme buildingScheme)
        {
            if (Scheme != null)
                throw new Exception("Clear ghost before changing it");

            Scheme = buildingScheme;
            _ghost = GameObject.Instantiate(Scheme.GetGhostPrefab(), transform);
        }

        public void ClearGhost()
        {
            Scheme = null;
            GameObject.Destroy(_ghost);
            _ghost = null;
        }

        protected void Update()
        {
            if (_ghost == null)
                return;

            var mousePosition = MainCameraController.Instance.ScreenToWorld(Input.mousePosition);
            var cell = new Vector2Int(Mathf.CeilToInt(mousePosition.x / Buildings.CellSize), Mathf.CeilToInt(mousePosition.y / Buildings.CellSize));
            var buildingPosition = Buildings.GetWorldPosition(cell);

            _ghost.transform.position = buildingPosition;
            _ghost.SetActive(Buildings.IsInside(Scheme, cell));

            if (_inputs.IsTapedOnLevel())
            {
                if (Buildings.CanBuild(Scheme, cell))
                {
                    Messages.Publish(Buildings.Build(Scheme, cell));
                    ClearGhost();
                }
            }
        }
    }
}