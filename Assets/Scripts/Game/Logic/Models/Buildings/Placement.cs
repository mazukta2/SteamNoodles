﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Models.Events;

namespace Assets.Scripts.Models.Buildings
{
    public class Placement
    {

        public Point Size { get; }
        public Rect Rect { get; }
        public List<Building> Buildings { get; } = new List<Building>();
        public History History { get; } = new History();


        public Placement(Point size)
        {
            Size = size;
            Rect = size.AsCenteredRect();
        }

        public Building Place(ConstructionScheme scheme, Point position)
        {
            var building = new Building(this, scheme, position);
            Buildings.Add(building);
            History.Add(new BuildingAddedEvent(building));
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
                return Rect.Y == position.Y || Rect.Y + 1 == position.Y;
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

        public class BuildingAddedEvent : IGameEvent
        {
            public BuildingAddedEvent(Building building)
            {
                Building = building;
            }

            public Building Building { get; set; }
        }
    }
}
