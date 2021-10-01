using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.States;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class Recipe
    {
        public float CurrentProgress => Get().CurrentProgress;
        public float MaxProgress => Get().MaxProgress;
        public IIngredientPrototype Ingredient => Get().Prototype.Ingredient;

        public uint Id { get; private set; }

        private State _state;

        private GameState Get() => _state.Get<GameState>(Id);
        public Recipe(State state, uint id)
        {
            _state = state;
            Id = id;
        }

        public Recipe(State state, IRecipePrototype recipe)
        {
            _state = state;
            (Id, _) = _state.Add(new GameState(recipe));
        }

        public bool IsOpen()
        {
            return CurrentProgress < MaxProgress;
        }

        public void Progress(float workProgress)
        {
            var newProgess = Math.Clamp(CurrentProgress + workProgress, 0, MaxProgress);
            var state = Get();
            state.CurrentProgress = newProgess;
            _state.Change<GameState>(Id, state);
        }

        public struct GameState : IStateEntity
        {
            public IRecipePrototype Prototype { get; }
            public float CurrentProgress { get; set; }
            public float MaxProgress { get; }

            public GameState(IRecipePrototype proto)
            {
                Prototype = proto;
                CurrentProgress = 0;
                MaxProgress = 100;
            }
        }
    }
}
