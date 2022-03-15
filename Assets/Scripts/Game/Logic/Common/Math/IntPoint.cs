using System;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    [Serializable]
    public struct IntPoint
    {
        public int X;
        public int Y;

        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public FloatRect AsCenteredRect()
        {
            return new FloatRect(-X / 2, -Y / 2, X, Y);
        }

        public static IntPoint operator +(IntPoint current, IntPoint other) => new IntPoint(current.X + other.X, current.Y + other.Y);
        public static bool operator ==(IntPoint current, IntPoint other) => current.X == other.X && current.Y == other.Y;
        public static bool operator !=(IntPoint current, IntPoint other) => current.X != other.X || current.Y != other.Y;

        public override bool Equals(object obj)
        {
            if (obj is IntPoint otherPoint)
                return this == otherPoint;

            return false;
        }

        public override int GetHashCode()
        {
            return X + Y;
        }
    }
}
