using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Game.Logic.Contexts;
using Assets.Scripts.Models.Buildings;
using System;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingScrollView : GameMonoBehaviour, IHandContext
    {
        [SerializeField] PrototypeLink _buildingButton;

        public void Set(HandViewModel hand)
        {
            _buildingButton.DestroySpawned();
            foreach (var item in hand.GetConstructions())
                _buildingButton.Create<BuildingSchemeView>(x => x.Set(item));
        }
        //private PlayerHandViewModel _hand;
        //private PlacementViewModel _placement;

        //public void Set(PlayerHandViewModel hand, PlacementViewModel placement)
        //{
        //}

        //private void OnClick(BuildingSchemeViewModel scheme)
        //{
        //    if (_placement.Ghost == scheme)
        //    {
        //        _placement.ClearGhost();
        //    }
        //    else if (_placement.Ghost != null)
        //    {
        //        _placement.ClearGhost();
        //        _placement.SetGhost(scheme);
        //    }
        //    else
        //    {
        //        _placement.SetGhost(scheme);
        //    }
        //}
    }
}