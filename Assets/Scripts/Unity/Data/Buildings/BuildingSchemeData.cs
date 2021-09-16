
using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Prototypes.Levels;
using GameUnity.Assets.Scripts.Unity.Common;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using UnityEngine;

namespace Assets.Scripts.Data.Buildings
{
    [CreateAssetMenu(menuName = "Game/" + nameof(BuildingSchemeData))]
    public class BuildingSchemeData : ScriptableObject, IConstructionPrototype
    {
        public Sprite BuildingIcon;
        public GameObject Ghost;
        public GameObject View;
        public Point Size;
        public Requirements Requirements;

        public ISprite HandIcon => new UnitySprite(BuildingIcon);

        Point IConstructionPrototype.Size => Size;
        Requirements IConstructionPrototype.Requirements => Requirements;
    }
}

