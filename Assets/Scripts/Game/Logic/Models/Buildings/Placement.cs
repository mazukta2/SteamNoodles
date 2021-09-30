using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Events;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.States.Game.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Assets.Scripts.Game.Logic.Models.Events.GameEvents;

namespace Game.Assets.Scripts.Game.Logic.Models.Buildings
{
    public class Placement
    {
        public event Action OnConstructionAdded = delegate { };
        public Point Size { get; }
        public Rect Rect { get; }
        public Construction[] Constructions => _state.Get().GetConstructions();

        private StateLink<ConstructionsState> _state;
        public Placement(StateLink<ConstructionsState> state, Point size)
        {
            Size = size;
            Rect = size.AsCenteredRect();
            _state = state;
            _state.Get().SubscribeToNewConstruction(HandleConstructionAdded);
        }

        public Construction Place(ConstructionScheme scheme, Point position)
        {
            return _state.Get().Place(scheme, position);
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

        private void HandleConstructionAdded(Construction construction)
        {
            OnConstructionAdded();
        }
    }
}
