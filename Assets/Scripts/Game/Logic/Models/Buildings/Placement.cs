using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.States.Events;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Buildings
{
    public class Placement
    {
        public event Action OnConstructionAdded = delegate { };
        public Point Size { get; }
        public Rect Rect { get; }
        public Construction[] Constructions => GetConstructions();

        private State _state;
        public Placement(State state, Point size)
        {
            Size = size;
            Rect = size.AsCenteredRect();
            _state = state;
            _state.Subscribe<Construction.GameState>(HandleConstructionAdded, StateEventType.Add);
        }

        public Construction Place(ConstructionScheme scheme, Point position)
        {
            return new Construction(_state, scheme.Protype, position);
        }

        public bool Contain(uint key)
        {
            return Constructions.Any(x => x.Id == key);
        }

        public bool CanPlace(ConstructionScheme scheme, Point position)
        {
            return scheme
                .GetOccupiedSpace(position)
                .All(otherPosition => IsFreeCell(scheme, otherPosition));
        }

        public bool IsFreeCell(ConstructionScheme scheme, Point position)
        {
            if (!Rect.IsInside(position))
                return false;

            if (Constructions.Any(otherBuilding => otherBuilding.GetOccupiedScace().Any(pos => pos == position)))
                return false;

            if (scheme.Requirements.DownEdge)
            {
                var min = Rect.Y;
                var max = Rect.Y + scheme.CellSize.Y;
                return min <= position.Y && position.Y < max;
            }

            return true;
        }

        private void HandleConstructionAdded(uint id, Construction.GameState construction)
        {
            OnConstructionAdded();
        }

        public Construction[] GetConstructions()
        {
            return _state.GetAllId<Construction.GameState>().Select(x => new Construction(_state, x)).ToArray();
        }
    }
}
