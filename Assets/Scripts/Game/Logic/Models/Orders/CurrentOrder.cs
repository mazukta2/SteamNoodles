using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.States.Game.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class CurrentOrder
    {
        public Recipe[] Recipes => _state.Get().GetRecipies();

        private StateLink<OrderState> _state;

        public CurrentOrder(StateLink<OrderState> state)
        {
            _state = state;
        }

        public bool Have(IIngredientPrototype ingredient)
        {
            return Recipes.Any(x => x.Ingredient == ingredient);
        }

        public bool IsOpen()
        {
            return Recipes.Any(x => x.IsOpen());
        }
    }
}
