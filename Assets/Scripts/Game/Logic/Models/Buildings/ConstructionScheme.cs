using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Assets.Scripts.Models.Buildings
{
    public class ConstructionScheme
    {
        
        private IConstructionPrototype _item;

        public ConstructionScheme(IConstructionPrototype item)
        {
            _item = item;
        }

        public Point CellSize => _item.Size;
        public Requirements Requirements => _item.Requirements;
        public ISprite HandIcon => _item.HandIcon;
        public IVisual BuildingView => _item.BuildingView;
        public IIngredientPrototype ProvidedIngridient => _item.ProvideIngredient;

        public IConstructionPrototype Protype => _item;

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
