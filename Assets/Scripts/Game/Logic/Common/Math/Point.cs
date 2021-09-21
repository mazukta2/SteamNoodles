using System;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    [Serializable]
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Rect AsCenteredRect()
        {
            return new Rect(-X / 2, -Y / 2, X, Y);
        }

        public static Point operator +(Point current, Point other) => new Point(current.X + other.X, current.Y + other.Y);
        public static bool operator ==(Point current, Point other) => current.X == other.X && current.Y == other.Y;
        public static bool operator !=(Point current, Point other) => current.X != other.X || current.Y != other.Y;

        public override bool Equals(object obj)
        {
            if (obj is Point otherPoint)
                return this == otherPoint;

            return false;
        }

        public override int GetHashCode()
        {
            return X + Y;
        }
    }
}
