using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using GameUnity.Assets.Scripts.Unity.Common;
using System;
using System.Linq;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using UnityEngine;

namespace Assets.Scripts.Data.Buildings
{
    [CreateAssetMenu(menuName = "Game/" + nameof(OrderData))]
    public class OrderData : ScriptableObject, IOrderPrototype
    {
        public Recipe[] Recipies;
        public IRecipePrototype[] Recipes => Recipies.Cast<IRecipePrototype>().ToArray();

        [Serializable]
        public struct Recipe : IRecipePrototype
        {
            public IngredientData Ingredient;
            IIngredientPrototype IRecipePrototype.Ingredient => Ingredient;
        }
    }
}

