using Assets.Scripts.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.States.Game.Level
{
    public struct JobState : IStateEntity
    {
        private State _state;
        public uint Id { get; }
        public uint Recipe { get; }

        public JobState(State state, uint id, uint recipe)
        {
            _state = state;
            Id = id;
            Recipe = recipe;
        }
    }
}
