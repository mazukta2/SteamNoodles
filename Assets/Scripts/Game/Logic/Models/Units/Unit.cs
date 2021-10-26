using Game.Assets.Scripts.Game.Logic.Common.Math;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class Unit
    {
        public Point Position { get; private set; }
        public Unit(Point position)
        {
            Position = position;
        }
    }
}
