using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Prototypes.Levels;
using System.Collections.Generic;

namespace Assets.Scripts.Models.Buildings
{
    public class ConstructionScheme
    {
        
        private IBuildingPrototype _item;

        public ConstructionScheme(IBuildingPrototype item)
        {
            _item = item;
        }

        public Point CellSize => _item.Size;
        public Requirements Requirements => _item.Requirements;

        public Point[] GetOccupiedSpace(Point position)
        {
            var result = new List<Point>();
            for (int x = 0; x < CellSize.X; x++)
            {
                for (int y = 0; y < CellSize.Y; y++)
                {
                    result.Add(position + new Point(x, y));
                }
            }
            return result.ToArray();
        }
    }
}
