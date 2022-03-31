using System;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    [Serializable]
    public struct FloatPoint
    {
        public static readonly FloatPoint Zero = new FloatPoint(0, 0);

        public float X;
        public float Y;

        public FloatPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static FloatPoint operator +(FloatPoint current, FloatPoint other) => new FloatPoint(current.X + other.X, current.Y + other.Y);
        public static FloatPoint operator -(FloatPoint current, FloatPoint other) => new FloatPoint(current.X - other.X, current.Y - other.Y);
        public static bool operator ==(FloatPoint current, FloatPoint other) => current.X == other.X && current.Y == other.Y;
        public static bool operator !=(FloatPoint current, FloatPoint other) => current.X != other.X || current.Y != other.Y;
        public static FloatPoint operator *(FloatPoint current, float other) => new FloatPoint(current.X * other, current.Y * other);


        public bool IsClose(FloatPoint target)
        {
            return System.Math.Abs(target.X - X) < 0.1f && System.Math.Abs(target.Y - Y) < 0.1f;
        }

        public FloatPoint GetNormalize()
        {
            var distance = (float)System.Math.Sqrt(X * X + Y * Y);
            return new FloatPoint(X / distance, Y / distance);
        }

        public override bool Equals(object obj)
        {
            if (obj is FloatPoint otherPoint)
                return this == otherPoint;

            return false;
        }

        public float GetDistanceTo(FloatPoint target)
        {
            return (float)System.Math.Sqrt((System.Math.Pow(X - target.X, 2) + System.Math.Pow(Y - target.Y, 2)));
        }

        public override int GetHashCode()
        {
            return (int)(X + Y);
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public bool IsZero()
        {
            return X == 0 && Y == 0;
        }
    }
}
