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

        private State _state;
        private uint _id;

        private GameState Get() => _state.Get<GameState>(_id);
        public Recipe(State state, uint id)
        {
            _state = state;
            _id = id;
        }

        public Construction GetConstruction()
        {
            return null;
            //return _level.Placement.Constructions.FirstOrDefault(x => x.IsProvide(_proto.Ingredient));
        }

        public bool IsOpen()
        {
            return CurrentProgress < MaxProgress;
        }

        public bool IsCanBeClosed()
        {
            return false;
            //return _level.Placement.Constructions.Any(x => x.IsProvide(_proto.Ingredient));
        }

        public void Progress(float workProgress)
        {
            var newProgess = Math.Clamp(CurrentProgress + workProgress, 0, MaxProgress);
            _state.Change<GameState>(_id, x => x.CurrentProgress = newProgess);
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
