using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class ActiveOrder
    {
        public event Action OnComplited = delegate {};

        private GameState _state;

        public ActiveOrder(AvailableOrder order)
        {
            _state = new GameState();
            _state.Prototype = order.GetPrototype();

            foreach(var recipe in order.GetPrototype().Recipes)
            {
                var newRecipe = new Recipe(recipe);
                newRecipe.OnComplited += () => HandleRecipeComplited(newRecipe);
                _state.Recipies.Add(newRecipe);
            }
        }

        public Recipe[] Recipes => _state.Recipies.ToArray();

        public bool Have(IIngredientPrototype ingredient)
        {
            return Recipes.Any(x => x.Ingredient == ingredient);
        }

        public bool IsOpen()
        {
            return Recipes.Any(x => x.IsOpen());
        }

        private void HandleRecipeComplited(Recipe newRecipe)
        {
            if (!_state.Recipies.Contains(newRecipe))
                throw new Exception("Recipe is not existing");

            if (!IsOpen())
                OnComplited();
        }

        private class GameState : IStateEntity
        {
            public IOrderPrototype Prototype { get; set; }
            public List<Recipe> Recipies { get; private set; } = new List<Recipe>();
        }
    }
}
