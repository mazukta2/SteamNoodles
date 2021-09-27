using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Assets.Scripts.Game.Logic.Models.Orders
{
    public class AvailableOrder
    {
        private IOrderPrototype _order;

        public AvailableOrder(IOrderPrototype order)
        {
            _order = order;
        }

        public CurrentOrder ToCurrentOrder()
        {
            return new CurrentOrder(_order);
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
    }
}
