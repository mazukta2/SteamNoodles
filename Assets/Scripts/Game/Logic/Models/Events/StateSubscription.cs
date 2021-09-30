using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Events
{
    public class StateSubscription<T> where T : IStateEntity
    {
        private State _state;
        private Action _sub;
        public StateSubscription(State state, Action sub)
        {
            _state = state;
            _sub = sub;
        }
    }
}
