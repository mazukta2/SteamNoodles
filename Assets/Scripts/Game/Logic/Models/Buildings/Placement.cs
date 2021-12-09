using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Buildings
{
    public class Placement : Disposable
    {
        public readonly float CellSize = 0.25f;

        public event Action<Construction> OnConstructionAdded = delegate { };
        public event Action<Construction> OnConstructionRemoved = delegate { };

        public Point Size => _prototype.Size;
        public Rect Rect => Size.AsCenteredRect();
        public FloatRect RealRect => Rect * CellSize;
        public IReadOnlyCollection<Construction> Constructions => _constructions.AsReadOnly();

        private IPlacementSettings _prototype { get; set; }
        private PlayerHand _hand { get; set; }
        private List<Construction> _constructions = new List<Construction>();

        public Placement(IPlacementSettings placement, PlayerHand hand)
        {
            _prototype = placement;
            _hand = hand;
        }

        protected override void DisposeInner()
        {
            foreach (var construction in _constructions)
                construction.Dispose();
            _constructions = null;
        }

        public Construction Place(ConstructionCard card, Point position)
        {
            var construction = new Construction(card.Protype, position);
            _constructions.Add(construction);
            if (_hand.Contain(card))
                _hand.Remove(card);

            OnConstructionAdded(construction);
            return construction;
        }

        public bool Contain(Construction key)
        {
            return Constructions.Any(x => x == key);
        }

        public bool CanPlace(ConstructionCard scheme, Point position)
        {
            return scheme
                .GetOccupiedSpace(position)
                .All(otherPosition => IsFreeCell(scheme, otherPosition));
        }

        public bool IsFreeCell(ConstructionCard scheme, Point position)
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
    }
}
