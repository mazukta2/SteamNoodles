using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using System;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    [Serializable]
    public struct FloatPoint3D
    {
        public static readonly FloatPoint3D Zero = new FloatPoint3D(0, 0, 0);
        public static readonly FloatPoint3D One = new FloatPoint3D(1, 1, 1);

        public float X;
        public float Y;
        public float Z;

        public FloatPoint3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static FloatPoint3D operator +(FloatPoint3D current, FloatPoint3D other) => new FloatPoint3D(current.X + other.X, current.Y + other.Y, current.Z + other.Z);
        public static FloatPoint3D operator -(FloatPoint3D current, FloatPoint3D other) => new FloatPoint3D(current.X - other.X, current.Y - other.Y, current.Z - other.Z);
        public static bool operator ==(FloatPoint3D current, FloatPoint3D other) => current.X == other.X && current.Y == other.Y && current.Z == other.Z;
        public static bool operator !=(FloatPoint3D current, FloatPoint3D other) => current.X != other.X || current.Y != other.Y || current.Z != other.Z;
        public static FloatPoint3D operator *(FloatPoint3D current, float other) => new FloatPoint3D(current.X * other, current.Y * other, current.Z * other);
        public static FloatPoint3D operator *(float other, FloatPoint3D current) => new FloatPoint3D(current.X * other, current.Y * other, current.Z * other);


        public bool IsClose(FloatPoint3D target)
        {
            return System.Math.Abs(target.X - X) < 0.1f && System.Math.Abs(target.Y - Y) < 0.1f && System.Math.Abs(target.Z - Z) < 0.1f;
        }

        public static FloatPoint3D Lerp(FloatPoint3D point1, FloatPoint3D point2, float value)
        {
            return new FloatPoint3D(MathHelpers.Lerp(point1.X, point2.X, value), MathHelpers.Lerp(point1.Y, point2.Y, value), MathHelpers.Lerp(point1.Z, point2.Z, value));
        }

        public FloatPoint3D GetNormalize()
        {
            var distance = (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
            return new FloatPoint3D(X / distance, Y / distance, Z / distance);
        }

        public override bool Equals(object obj)
        {
            if (obj is FloatPoint3D otherPoint)
                return this == otherPoint;

            return false;
        }

        public float GetDistanceTo(FloatPoint3D target)
        {
            return (float)System.Math.Sqrt((System.Math.Pow(X - target.X, 2) + System.Math.Pow(Y - target.Y, 2) + System.Math.Pow(Z - target.Z, 2)));
        }

        public override int GetHashCode()
        {
            return (int)(X + Y + Z);
        }

        public override string ToString()
        {
            return $"[{X}, {Y}, {Z}]";
        }

        public bool IsZero()
        {
            return X == 0 && Y == 0 && Z == 0;
        }
    }
}
