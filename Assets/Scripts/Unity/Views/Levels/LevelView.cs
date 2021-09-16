using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Views.Buildings;
using Assets.Scripts.Views.Buildings.Grid;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using UnityEngine;

namespace Assets.Scripts.Views.Levels
{
    public class LevelView : GameMonoBehaviour, ILevelView
    {
        [SerializeField] PrototypeLink _gridPrototype;
        [SerializeField] PrototypeLink _buildingPanel;
        [SerializeField] PrototypeLink _ghostPrototype;


        public void CreateHand(PlayerHand hand)
        {
            //_buildingPanel.Create<BuildingScrollView>(v => v.Set(hand));
        }

        public IHandView CreateHand()
        {
            return _buildingPanel.Create<BuildingScrollView>();
        }

        public IPlacementView CreatePlacement()
        {
            return _gridPrototype.Create<GridView>();
        }


        //protected void OnEnable()
        //{
        //    _gridPrototype.Create<GridView>(v => v.Set(_level.Placement));
        //    _ghostPrototype.Create<BuildingGhostView>(v => v.Set(_level.Placement));
        //}


    }
}