using Assets.Scripts.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.States.Game.Level
{
    public struct RecipeState : IStateEntity
    {
        private State _state;
        public uint Id { get; }
        public IRecipePrototype Prototype { get; }
        public float CurrentProgress { get; set; }
        public float MaxProgress { get; }

        public RecipeState(State state, uint id, IRecipePrototype proto)
        {
            _state = state;
            Id = id;
            Prototype = proto;
            CurrentProgress = 0;
            MaxProgress = 100;
        }
    }
}
