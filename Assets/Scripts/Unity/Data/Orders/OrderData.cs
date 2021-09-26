using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using GameUnity.Assets.Scripts.Unity.Common;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using UnityEngine;

namespace Assets.Scripts.Data.Buildings
{
    [CreateAssetMenu(menuName = "Game/" + nameof(OrderData))]
    public class OrderData : ScriptableObject, IOrderPrototype
    {
        public IngredientData[] Ingredients;
        public IIngredientPrototype[] RequiredIngredients => Ingredients;
    }
}

