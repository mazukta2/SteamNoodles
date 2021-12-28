using UnityEngine;

namespace Assets.Scripts.Data.Buildings
{
    [CreateAssetMenu(menuName = "Game/" + nameof(OrderData))]
    public class OrderData : ScriptableObject
    {
        //public Recipe[] Recipies;
        //public IRecipePrototype[] Recipes => Recipies.Cast<IRecipePrototype>().ToArray();

        //[Serializable]
        //public struct Recipe : IRecipePrototype
        //{
        //    public IngredientData Ingredient;
        //    public int Count;
        //    IIngredientPrototype IRecipePrototype.Ingredient => Ingredient;
        //    int IRecipePrototype.Count => Count;
        //}
    }
}

