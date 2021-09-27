using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class AvailableOrder
    {
        private IOrderPrototype _order;
        private GameLevel _level;

        public AvailableOrder(GameLevel level, IOrderPrototype order)
        {
            _order = order;
            _level = level;
        }

        public CurrentOrder ToCurrentOrder()
        {
            return new CurrentOrder(_level, _order);
        }

        public bool CanBeOrder(Construction[] constructions)
        {
            foreach (var recipe in _order.Recipes)
            {
                if (!constructions.Any(x => x.IsProvide(recipe.Ingredient)))
                    return false;
            }
            return true;
        }

        public bool Have(IIngredientPrototype ingredient)
        {
            return _order.Recipes.Any(x => x.Ingredient == ingredient);
        }
    }
}
