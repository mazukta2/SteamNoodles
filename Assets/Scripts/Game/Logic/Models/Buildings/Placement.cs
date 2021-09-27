using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Tests.Assets.Scripts.Game.Logic.Models.Events.GameEvents;
using Tests.Assets.Scripts.Game.Logic.Models.Orders;

namespace Assets.Scripts.Models.Buildings
{
    public class Placement
    {
        public Point Size { get; }
        public Rect Rect { get; }
        public GameLevel Level { get; }
        public List<Construction> Constructions { get; } = new List<Construction>();
        public History History { get; } = new History();


        public Placement(GameLevel level, Point size)
        {
            Size = size;
            Rect = size.AsCenteredRect();
            Level = level;
        }

        public Construction Place(ConstructionScheme scheme, Point position)
        {
            var building = new Construction(this, scheme, position);
            Constructions.Add(building);

            Level.Orders.TryGetOrder();

            History.Add(new ConstrcutionAddedEvent(building));
            return building;
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

    }
}
