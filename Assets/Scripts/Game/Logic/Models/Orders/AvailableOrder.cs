using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class AvailableOrder
    {
        private IOrderPrototype _order;
        private Placement _placement;

        public AvailableOrder(Placement placement, IOrderPrototype order)
        {
            _order = order;
            _placement = placement;
        }

        public bool CanBeOrder()
        {
            foreach (var recipe in _order.Recipes)
            {
                if (!_placement.GetConstructions().Any(x => x.IsProvide(recipe)))
                    return false;
            }
            return true;
        }

        public bool Have(IIngredientPrototype ingredient)
        {
            return _order.Recipes.Any(x => x.Ingredient == ingredient);
        }

        public IOrderPrototype GetPrototype()
        {
            return _order;
        }

    }
}
