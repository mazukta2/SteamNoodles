using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ViewModels.Buildings
{
    public class BuildingSchemeViewModel : BuildingScheme
    {
        public Sprite BuildingIcon { get; }
        public GameObject Ghost { get; }
        public GameObject View { get; }
        /*
        public BuildingSchemeViewModel(IBuildingSchemeViewData data) : base(data)
        {
            BuildingIcon = data.BuildingIcon;
            Ghost = data.Ghost;
            View = data.View;
        }
        */
    }
}
