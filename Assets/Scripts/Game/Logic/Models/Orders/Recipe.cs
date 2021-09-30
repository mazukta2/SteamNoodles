using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.States.Game.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class Recipe
    {
        public float CurrentProgress => _state.Get().CurrentProgress;
        public float MaxProgress => _state.Get().MaxProgress;
        public IIngredientPrototype Ingredient => _state.Get().Prototype.Ingredient;

        private StateLink<RecipeState> _state;

        public Recipe(StateLink<RecipeState> state)
        {
            _state = state;
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
            _state.Change(x => x.CurrentProgress = newProgess);
        }
    }
}
