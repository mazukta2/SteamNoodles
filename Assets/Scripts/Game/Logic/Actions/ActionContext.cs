using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Actions
{
    public class ActionContext<T>
    {
        private State _state;
        private T _model;

        public ActionContext(State state, T model)
        {
            _state = state;
            _model = model;
        }

        public void Execute(GameAction action)
        {
            action.SetContext(_state);
            action.Execute();
        }

        public T GetModel()
        {
            return _model;
        }
    }
}
