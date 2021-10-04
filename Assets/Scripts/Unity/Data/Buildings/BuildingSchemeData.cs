using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
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
        public GameObject View;
        public Point Size;
        public Requirements Requirements;
        [Header("Craft")]
        public float TimeBeforeHits = 2;
        public float WorkPerHit = 1;
        public IngredientData Ingredient;

        public ISprite HandIcon => new UnitySprite(BuildingIcon);
        public IVisual BuildingView => new UnityView(View);

        public IIngredientPrototype ProvideIngredient => Ingredient;

        public float WorkTime => TimeBeforeHits;
        public float WorkProgressPerHit => WorkPerHit;

        Point IConstructionPrototype.Size => Size;
        Requirements IConstructionPrototype.Requirements => Requirements;
    }
}

