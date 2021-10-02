using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.States;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class Recipe
    {
        public event Action OnComplited = delegate { };
        private GameState _state;

        public Recipe(IRecipePrototype recipe)
        {
            _state = new GameState();
            _state.Prototype = recipe;
        }

        public float CurrentProgress => _state.CurrentProgress;
        public float MaxProgress => 100;
        public IIngredientPrototype Ingredient => _state.Prototype.Ingredient;

        public bool IsOpen()
        {
            return CurrentProgress < MaxProgress;
        }

        public void Progress(float workProgress)
        {
            _state.CurrentProgress = Math.Clamp(CurrentProgress + workProgress, 0, MaxProgress);
            if (!IsOpen())
                OnComplited();
        }

        private class GameState : IStateEntity
        {
            public IRecipePrototype Prototype { get; set; }
            public float CurrentProgress { get; set; }
            public uint Order { get; }
        }

    }
}
