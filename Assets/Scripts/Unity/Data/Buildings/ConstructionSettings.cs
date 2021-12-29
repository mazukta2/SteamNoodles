using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;

namespace GameUnity.Assets.Scripts.Unity.Data.Buildings
{
    public class ConstructionSettings : IConstructionSettings
    {
        //public Sprite BuildingIcon;
        //public GameObject View;
        //public Point Size;
        //public Requirements Requirements;
        //[Header("Craft")]
        //public float TimeBeforeHits = 2;
        //public float WorkPerHit = 1;
        //public IngredientData Ingredient;

        //public ISprite HandIcon => new UnitySprite(BuildingIcon);
        //public IVisual BuildingView => new UnityView(View);

        //public IIngredientPrototype ProvideIngredient => Ingredient;

        //public float WorkTime => TimeBeforeHits;
        //public float WorkProgressPerHit => WorkPerHit;

        //Point IConstructionPrototype.Size => Size;
        //Requirements IConstructionPrototype.Requirements => Requirements;
        public Point Size => throw new NotImplementedException();
        public Requirements Requirements => throw new NotImplementedException();
        public ISprite HandIcon => throw new NotImplementedException();
        public IVisual BuildingView => throw new NotImplementedException();
        public IReadOnlyCollection<IConstructionFeatureSettings> Features => throw new NotImplementedException();
        public IReadOnlyDictionary<ConstructionTag, int> Tags => throw new NotImplementedException();
    }
}

