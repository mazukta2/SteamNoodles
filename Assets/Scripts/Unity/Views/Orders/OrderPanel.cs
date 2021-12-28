using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Orders
{
    public class OrderPanel : ViewMonoBehaviour, ICurrentOrderView
    {
        [SerializeField] PrototypeLink _recipeView;
        //public IRecipeView CreateRecipe()
        //{
        //    return _recipeView.Create<RecipeView>();
        //}
    }
}
