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
            return new CurrentOrder(this);
        }

        public bool CanBeOrder(Construction[] constructions)
        {
            foreach (var ingredient in _order.RequiredIngredients)
            {
                if (!constructions.Any(x => x.IsProvide(ingredient)))
                    return false;
            }
            return true;
        }
    }
}
