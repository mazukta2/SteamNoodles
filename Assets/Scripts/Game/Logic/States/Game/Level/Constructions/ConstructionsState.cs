using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.States.Game.Level
{
    public struct ConstructionsState : IStateEntity
    {
        private State _state;
        public uint Id { get; }

        public ConstructionsState(State state, uint id)
        {
            _state = state;
            Id = id;
        }

        public Construction[] GetConstructions()
        {
            return _state.GetAll<ConstructionState>().Select(x => new Construction(x)).ToArray();
        }

        public Construction Place(ConstructionScheme scheme, Point position)
        {
            return new Construction(_state.Add((s, i) => new ConstructionState(s, i, scheme.Protype, position)));
        }

        public void SubscribeToNewConstruction(Action<Construction> action)
        {
            var state = _state;
            _state.SubscribeToNew<ConstructionState>((c) => action(new Construction(new StateLink<ConstructionState>(state, c.Id))));
        }
    }
}
