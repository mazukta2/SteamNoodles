using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models.Buildings
{
    public class PlayerHand
    {
        public event Action<ConstructionScheme> OnAdded = delegate { };
        public event Action<ConstructionScheme> OnRemoved = delegate { };

        public ConstructionScheme[] CurrentSchemes => _state.Schemes.ToArray();

        private GameState _state;

        public PlayerHand(IConstructionSettings[] startingHand)
        {
            _state = new GameState();

            foreach (var item in startingHand)
            {
                Add(new ConstructionScheme(item));
            }
        }

        public void Remove(ConstructionScheme scheme)
        {
            _state.Schemes.Remove(scheme);
            OnRemoved(scheme);
        }

        public bool Contain(ConstructionScheme scheme)
        {
            return _state.Schemes.Contains(scheme);
        }

        public void Add(IConstructionSettings proto)
        {
            Add(new ConstructionScheme(proto));
        }

        private void Add(ConstructionScheme buildingScheme)
        {
            _state.Schemes.Add(buildingScheme);
            OnAdded(buildingScheme);
        }

        private class GameState : IStateEntity
        {
            public List<ConstructionScheme> Schemes { get; set; } = new List<ConstructionScheme>();
        }
    }
}
