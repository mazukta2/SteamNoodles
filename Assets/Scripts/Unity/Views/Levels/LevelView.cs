using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Views.Buildings;
using Assets.Scripts.Views.Buildings.Grid;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using GameUnity.Assets.Scripts.Unity.Views.Orders;
using System;
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
        private Action<float> _moveTime;

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

        public void SetTimeMover(Action<float> moveTime)
        {
            _moveTime = moveTime;
        }

        protected void Update()
        {
            if (_moveTime != null)
                _moveTime(Time.deltaTime);
        }
    }
}