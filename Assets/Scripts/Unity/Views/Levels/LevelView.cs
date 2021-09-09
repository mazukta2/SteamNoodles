using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.ViewModels.Level;
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

        //private LevelViewModel _level;

        //public void Set(GameLevel level)
        //{
        //    if (level == null) throw new ArgumentNullException(nameof(level));

        //    _level = new LevelViewModel(level);
        //}

        //protected void OnEnable()
        //{
        //    _gridPrototype.Create<GridView>(v => v.Set(_level.Placement));
        //    _buildingPanel.Create<BuildingScrollView>(v => v.Set(_level.Hand, _level.Placement));
        //    _ghostPrototype.Create<BuildingGhostView>(v => v.Set(_level.Placement));
        //}


    }
}