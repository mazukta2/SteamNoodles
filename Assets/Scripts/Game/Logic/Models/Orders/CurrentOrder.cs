using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class CurrentOrder
    {
        public Recipe[] Recipes => _recipes.ToArray();
        public History History = new History();

        private List<Recipe> _recipes = new List<Recipe>();
        private IOrderPrototype _order;
        private bool _isOpen;

        public CurrentOrder(GameLevel level, IOrderPrototype order)
        {
            _order = order;
            _isOpen = true;
            foreach (var rec in _order.Recipes)
                _recipes.Add(new Recipe(level, rec));
        }

        public bool Have(IIngredientPrototype ingredient)
        {
            return _order.Recipes.Any(x => x.Ingredient == ingredient);
        }

        public bool IsOpen()
        {
            return _isOpen;
        }

        public bool IsCanBeClosed()
        {
            return _recipes.All(x => !x.IsOpen());
        }

        public void Close()
        {
            if (!IsCanBeClosed())
                throw new Exception("Cant be closed");

            _isOpen = false;

            History.Add(new CurrentOrderClosedEvent(this));
        }
    }
}
