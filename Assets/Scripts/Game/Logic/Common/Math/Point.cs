
namespace Assets.Scripts.Game.Logic.Common.Math
{
    public struct Point
    {
        public readonly int X { get; }
        public readonly int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Rect AsCenteredRect()
        {
            return new Rect(-X / 2, -Y / 2, X, Y);
        }
    }
}
