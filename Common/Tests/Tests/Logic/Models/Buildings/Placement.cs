using System.Collections.Generic;
using Assets.Scripts.Logic.Common.Math;
using Assets.Scripts.Models.Events;

namespace Assets.Scripts.Models.Buildings
{
    public class Placement
    {
        public readonly float CellSize = 0.25f;

        public IPoint Size { get; }
        public IRect Rect { get; }
        public List<Building> Buildings { get; } = new List<Building>();
        public History History { get; } = new History();

        //public Placement(BuildingsData data)
        //{
        //    Size = data.MapSize;
        //}

        public Placement(IPoint size)
        {
            Size = size;
            //Rect = new RectInt(-Size.x / 2, -Size.y / 2, Size.x, Size.y);
        }

        public Placement(Placement origin)
        {
            Size = origin.Size;
            Rect = origin.Rect;
        }

        //public IPoint GetWorldPosition(IPoint cell)
        //{
        //    return new Vector3(cell.x * CellSize - CellSize / 2, cell.y * CellSize - CellSize / 2);
        //}

        public Building Place(BuildingScheme scheme, IPoint position)
        {
            var building = new Building(this, scheme, position);
            Buildings.Add(building);
            History.Add(new BuildingAddedEvent(building));
            return building;
        }

        public bool CanPlace(BuildingScheme scheme, IPoint position)
        {
            return false;
            //return scheme
              //  .GetOccupiedSpace(position)
                //.All(otherPosition => IsFreeCell(scheme, otherPosition));
        }

        public bool IsFreeCell(BuildingScheme scheme, IPoint position)
        {
            /*
            if (!Rect.IsInside(position))
                return false;

            if (Buildings.Any(otherBuilding => otherBuilding.GetOccupiedScace().Any(pos => pos == position)))
                return false;

            if (scheme.Requirements.DownEdge)
            {
                return Rect.y == position.y || Rect.y + 1 == position.y;
            }*/

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
