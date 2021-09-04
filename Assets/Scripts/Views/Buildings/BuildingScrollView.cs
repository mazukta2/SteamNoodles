using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Models.Buildings;
using System;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingScrollView : GameMonoBehaviour
    {
        [SerializeField] PrototypeLink _buildingButton;
        private BuildingViewModel _building;

        public void Set(BuildingViewModel building)
        {
            if (building == null) throw new ArgumentNullException(nameof(building));
            _building = building;
            foreach (var item in _building.GetPool().GetCurrentSchemes())
                _buildingButton.Create<BuildingSchemeView>(x => x.Set(item, OnClick));
        }

        private void OnClick(BuildingScheme scheme)
        {
            if (_building.Ghost == scheme)
            {
                _building.ClearGhost();
            }
            else if (_building.Ghost != null)
            {
                _building.ClearGhost();
                _building.SetGhost(scheme);
            }
            else
            {
                _building.SetGhost(scheme);
            }
        }
    }
}