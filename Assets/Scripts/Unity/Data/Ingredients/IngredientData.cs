using Assets.Scripts.Logic.Prototypes.Levels;
using System;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Data.Ingredients
{
    [CreateAssetMenu(menuName = "Game/" + nameof(IngredientData))]
    public class IngredientData : ScriptableObject
    {
        [SerializeField] string _name;
        public string Name => _name;
    }
}

