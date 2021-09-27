using Assets.Scripts.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Mocks.Prototypes.Levels;

namespace Tests.Assets.Scripts.Game.Logic.Models.Orders
{
    public class CurrentOrder
    {
        public Recipe[] Recipes => _recipes.ToArray();

        private List<Recipe> _recipes = new List<Recipe>();
        private IOrderPrototype _order;

        public CurrentOrder(IOrderPrototype order)
        {
            _order = order;
            foreach (var rec in _order.Recipes)
                _recipes.Add(new Recipe(rec));
        }

        public bool Have(IIngredientPrototype ingredient)
        {
            return _order.Recipes.Any(x => x.Ingredient == ingredient);
        }
    }
}
