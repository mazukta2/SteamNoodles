using Assets.Scripts.Core;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;
using Assets.Scripts.Models.Requests;
using System;
using UnityEngine;
using UniRx;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingsView : GameMonoBehaviour, IBuildingsRequest
    {
        public SessionBuildings Buildings { get; set; }

        protected void Start()
        {
            Messages.Publish<IBuildingsRequest>(this);
            Messages.Receive<BuildingPlacedEvent>().Subscribe(HandleBuildingPlaced).AddTo(this);

            RecreateBuildings();
        }

        private void HandleBuildingPlaced(BuildingPlacedEvent building) => AddBuilding(building.Building);

        private void RecreateBuildings()
        {
            foreach (var building in Buildings.GetBuildings())
                AddBuilding(building);
        }

        private void AddBuilding(Building building)
        {
            var view = GameObject.Instantiate(building.Scheme.View, transform);
            view.transform.position = Buildings.GetWorldPosition(building.Position);
        }

    }
}