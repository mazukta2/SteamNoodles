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
        [SerializeField] PrototypeLink _gridPrototype;
        [SerializeField] PrototypeLink _buildingPanel;
        [SerializeField] PrototypeLink _unitsPrototype;
        [SerializeField] PrototypeLink _clashesPrototyp;

        public IScreenView Screen => throw new NotImplementedException();
        public DisposableViewKeeper<IPlacementView> Placement => throw new NotImplementedException();

        public IClashesView CreateClashes()
        {
            return _clashesPrototyp.Create<ClashesView>();
        }

        public IHandView CreateHand()
        {
            return _buildingPanel.Create<BuildingScrollView>();
        }

        public IPlacementView CreatePlacement()
        {
            return _gridPrototype.Create<GridView>();
        }

        public IUnitsView CreateUnits()
        {
            return _unitsPrototype.Create<UnitsView>();
        }
    }
}