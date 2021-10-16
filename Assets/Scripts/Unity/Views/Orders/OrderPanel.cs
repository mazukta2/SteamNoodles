using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Views.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Orders
{
    public class OrderPanel : GameMonoBehaviour, ICurrentOrderView
    {
        [SerializeField] PrototypeLink _recipeView;
        public IRecipeView CreateRecipe()
        {
            return _recipeView.Create<RecipeView>();
        }
    }
}
