using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Levels;
using Assets.Scripts.Views.Buildings;
using Assets.Scripts.Views.Buildings.Grid;
using System;
using System.Collections;
using UnityEngine;
using static Assets.Scripts.Views.Levels.LevelView;

namespace Assets.Scripts.Views.Levels
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] PrototypeLink _gridPrototype;
        [SerializeField] PrototypeLink _buildingPanel;
        [SerializeField] PrototypeLink _ghostPrototype;

        public GameLevel Level { get; private set; }

        private BuildingViewModel _building;

        public void Set(GameLevel level)
        {
            if (level == null) throw new ArgumentNullException(nameof(level));

            Level = level;
            _building = new BuildingViewModel(level);
        }

        protected void OnEnable()
        {
            _gridPrototype.Create<GridView>(v => v.Set(_building));
            _buildingPanel.Create<BuildingScrollView>(v => v.Set(_building));
            _ghostPrototype.Create<BuildingGhostView>(v => v.Set(_building));
        }


    }
}