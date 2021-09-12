using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Game.Logic.Contexts;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Views.Buildings;
using UnityEngine;

namespace Assets.Scripts.Views.Levels
{
    public class LevelView : MonoBehaviour, ILevelContext 
    {
        [SerializeField] PrototypeLink _gridPrototype;
        [SerializeField] PrototypeLink _buildingPanel;
        [SerializeField] PrototypeLink _ghostPrototype;

        public void CreateHand(PlayerHand hand)
        {
            _buildingPanel.Create<BuildingScrollView>(v => v.Set(hand));
        }

        //protected void OnEnable()
        //{
        //    _gridPrototype.Create<GridView>(v => v.Set(_level.Placement));
        //    _ghostPrototype.Create<BuildingGhostView>(v => v.Set(_level.Placement));
        //}


    }
}