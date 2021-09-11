
using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Prototypes.Levels;
using System;
using UnityEngine;

namespace Assets.Scripts.Data.Buildings
{
    [CreateAssetMenu(menuName = "Game/" + nameof(BuildingSchemeData))]
    public class BuildingSchemeData : ScriptableObject, IBuildingPrototype
    {
        public Sprite BuildingIcon;
        public GameObject Ghost;
        public GameObject View;
        public Point Size;
        public Requirements Requirements;

        Point IBuildingPrototype.Size => Size;
        Logic.Prototypes.Levels.Requirements IBuildingPrototype.Requirements => Requirements;
    }
}

