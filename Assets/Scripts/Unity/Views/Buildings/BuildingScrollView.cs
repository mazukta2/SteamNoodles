using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.ViewModels.Buildings;
using Assets.Scripts.ViewModels.Level;
using System;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingScrollView : GameMonoBehaviour
    {
        [SerializeField] PrototypeLink _buildingButton;
        //private PlayerHandViewModel _hand;
        //private PlacementViewModel _placement;

        //public void Set(PlayerHandViewModel hand, PlacementViewModel placement)
        //{
        //    if (hand == null) throw new ArgumentNullException(nameof(hand));
        //    if (placement == null) throw new ArgumentNullException(nameof(placement));
        //    _hand = hand;
        //    _placement = placement;

        //    foreach (var item in _hand.CurrentSchemes)
        //        _buildingButton.Create<BuildingSchemeView>(x => x.Set(item, OnClick));
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