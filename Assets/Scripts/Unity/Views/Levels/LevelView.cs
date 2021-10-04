using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Views.Buildings;
using Assets.Scripts.Views.Buildings.Grid;
using GameUnity.Assets.Scripts.Unity.Views.Orders;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using UnityEngine;

namespace Assets.Scripts.Views.Levels
{
    public class LevelView : GameMonoBehaviour, ILevelView
    {
        [SerializeField] PrototypeLink _gridPrototype;
        [SerializeField] PrototypeLink _buildingPanel;
        [SerializeField] PrototypeLink _orderPanel;

        public ICurrentOrderView CreateCurrentOrder()
        {
            return _orderPanel.Create<OrderPanel>();
        }

        public IHandView CreateHand()
        {
            return _buildingPanel.Create<BuildingScrollView>();
        }

        public IPlacementView CreatePlacement()
        {
            return _gridPrototype.Create<GridView>();
        }
    }
}