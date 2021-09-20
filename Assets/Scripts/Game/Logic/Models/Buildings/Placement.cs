using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Models.Events;
using Tests.Assets.Scripts.Game.Logic.Models.Events.GameEvents;

namespace Assets.Scripts.Models.Buildings
{
    public class Placement
    {

        public Point Size { get; }
        public Rect Rect { get; }
        public List<Construction> Buildings { get; } = new List<Construction>();
        public History History { get; } = new History();


        public Placement(Point size)
        {
            Size = size;
            Rect = size.AsCenteredRect();
        }

        public Construction Place(ConstructionScheme scheme, Point position)
        {
            var building = new Construction(this, scheme, position);
            Buildings.Add(building);
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

            if (Buildings.Any(otherBuilding => otherBuilding.GetOccupiedScace().Any(pos => pos == position)))
                return false;

            if (scheme.Requirements.DownEdge)
            {
                var min = Rect.Y;
                var max = Rect.Y + scheme.CellSize.Y;
                return min <= position.Y && position.Y < max;
            }

            return true;
        }


        //public IPoint[] GetPlaceablePositions(BuildingScheme scheme)
        //{
        //    var list = new List<IPoint>();
        //    for (int x = Rect.x; x < Rect.x + Rect.width; x++)
        //    {
        //        for (int y = Rect.y; y < Rect.y + Rect.height; y++)
        //        {
        //            var pos = new IPoint(x, y);
        //            if (IsFreeCell(scheme, pos))
        //                list.Add(pos);
        //        }
        //    }
        //    return list.ToArray();
        //}

    }
}
