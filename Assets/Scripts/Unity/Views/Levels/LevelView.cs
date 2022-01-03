using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Views.Buildings;
using Assets.Scripts.Views.Buildings.Grid;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using GameUnity.Assets.Scripts.Unity.Views.Orders;
using GameUnity.Assets.Scripts.Unity.Views.Units;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;

namespace Assets.Scripts.Views.Levels
{
    public class LevelView : ViewMonoBehaviour, ILevelView
    {
        [SerializeField] ScreenView _screenView;
        [SerializeField] UnitsView _unitsView;
        [SerializeField] PlacementView _placementView;

        public IScreenView Screen => _screenView;
        public IUnitsView Units => _unitsView;
        IPlacementView ILevelView.Placement => _placementView;


    }
}