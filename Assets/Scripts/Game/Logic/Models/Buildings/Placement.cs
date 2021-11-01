using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.States.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Buildings
{
    public class Placement
    {
        public readonly float CellSize = 0.25f;

        public event Action<Construction> OnConstructionAdded = delegate { };
        public event Action<Construction> OnConstructionRemoved = delegate { };

        private GameState _state;

        public Placement(IPlacementPrototype placement, PlayerHand hand)
        {
            _state = new GameState();
            _state.Prototype = placement;
            _state.Hand = hand;
        }

        public Point Size => _state.Prototype.Size;
        public Rect Rect => Size.AsCenteredRect();
        public FloatRect RealRect => Rect * CellSize;
        public Construction[] Constructions => _state.Constructions.ToArray();

        public Construction Place(ConstructionScheme scheme, Point position)
        {
            var construction = new Construction(scheme.Protype, position);
            _state.Constructions.Add(construction);
            if (_state.Hand.Contain(scheme))
                _state.Hand.Remove(scheme);

            OnConstructionAdded(construction);
            return construction;
        }

        public bool Contain(Construction key)
        {
            return Constructions.Any(x => x == key);
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

        private class GameState : IStateEntity
        {
            public IPlacementPrototype Prototype { get; set; }
            public List<Construction> Constructions { get; set; } = new List<Construction>();
            public PlayerHand Hand { get; set; }
        }
    }
}
