using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Events;
using Game.Assets.Scripts.Game.Logic.States.Game.Level;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.States
{
    public class StateLink<T> where T : IStateEntity
    {
        public T Get() => _state.GetById<T>(_id);

        private State _state;
        private uint _id;

        public StateLink(State state, uint id)
        {
            _state = state;
            _id = id;
        }

        public void Change(Action<T> p)
        {
            _state.Change(_id, p);
        }

    }
}
