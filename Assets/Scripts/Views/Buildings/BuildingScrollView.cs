using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Requests;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingScrollView : GameMonoBehaviour, IBuildingsRequest
    {
        public SessionBuildings Buildings { get; set; }
        [SerializeField] PrototypeLink _buildingButton;

        protected void Start()
        {
            Messages.Publish<IBuildingsRequest>(this);
            foreach (var item in Buildings.GetCurrentSchemes())
            {
                _buildingButton.Create<BuildingSchemeView>(x => x.Set(item));
            }
        }
    }
}