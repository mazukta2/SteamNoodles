using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Assets.Scripts.Models.Buildings
{
    public class ConstructionScheme
    {
        private GameState _state;

        public ConstructionScheme(IConstructionSettings item)
        {
            _state = new GameState();
            _state.Item = item;
        }

        public Point CellSize => _state.Item.Size;
        public Requirements Requirements => _state.Item.Requirements;
        public ISprite HandIcon => _state.Item.HandIcon;
        public IVisual BuildingView => _state.Item.BuildingView;
        public IConstructionSettings Protype => _state.Item;

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

        private class GameState : IStateEntity
        {
            public IConstructionSettings Item { get; set; }
        }
    }
}
