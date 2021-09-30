using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Prototypes.Levels;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class AvailableOrder
    {
        //private StateLink<LevelState> _state;
        private IOrderPrototype _order;
        private GameLevel _level;

        public AvailableOrder(IOrderPrototype order)
        {
            //_state = state;
            _order = order;
        }

        //public CurrentOrder ToCurrentOrder()
        //{
        //    return _state.Create() new CurrentOrder(_level, _order);
        //}

        public bool CanBeOrder()
        {
            //foreach (var recipe in _order.Recipes)
            //{
            //    if (!constructions.Any(x => x.IsProvide(recipe.Ingredient)))
            //        return false;
            //}
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
