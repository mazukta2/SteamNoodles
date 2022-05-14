using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using System;
using System.Numerics;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    [Serializable]
    public struct GameVector3
    {
        public static readonly GameVector3 Zero = new GameVector3(0, 0, 0);
        public static readonly GameVector3 One = new GameVector3(1, 1, 1);
        public static readonly GameVector3 Up = new GameVector3(0, 1, 0);
        public static readonly GameVector3 Forward = new GameVector3(0, 0, 1);

        public float X;
        public float Y;
        public float Z;


        public GameVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static GameVector3 operator +(GameVector3 current, GameVector3 other) => new GameVector3(current.X + other.X, current.Y + other.Y, current.Z + other.Z);

        public static GameVector3 operator -(GameVector3 current, GameVector3 other) => new GameVector3(current.X - other.X, current.Y - other.Y, current.Z - other.Z);
        public static bool operator ==(GameVector3 current, GameVector3 other) => current.X == other.X && current.Y == other.Y && current.Z == other.Z;
        public static bool operator !=(GameVector3 current, GameVector3 other) => current.X != other.X || current.Y != other.Y || current.Z != other.Z;
        public static GameVector3 operator *(GameVector3 current, float other) => new GameVector3(current.X * other, current.Y * other, current.Z * other);
        public static GameVector3 operator *(float other, GameVector3 current) => new GameVector3(current.X * other, current.Y * other, current.Z * other);
        public static GameVector3 operator /(GameVector3 current, float other) => new GameVector3(current.X / other, current.Y / other, current.Z / other);


        public bool IsClose(GameVector3 target)
        {
            return System.Math.Abs(target.X - X) < 0.1f && System.Math.Abs(target.Y - Y) < 0.1f && System.Math.Abs(target.Z - Z) < 0.1f;
        }

        public static GameVector3 Lerp(GameVector3 point1, GameVector3 point2, float value)
        {
            return new GameVector3(MathHelpers.Lerp(point1.X, point2.X, value), MathHelpers.Lerp(point1.Y, point2.Y, value), MathHelpers.Lerp(point1.Z, point2.Z, value));
        }

        public GameVector3 GetNormalize()
        {
            var distance = (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
            return new GameVector3(X / distance, Y / distance, Z / distance);
        }

        public GameVector3 MoveTowards(GameVector3 target, float speed)
        {
            var direction = target - this;
            var normalizeDirection = direction.GetNormalize();
            var distanceToMove = normalizeDirection * speed;
            if (GetDistanceTo(target) > speed)
                return this + normalizeDirection * speed;
            else
                return target;
        }

        public override bool Equals(object obj)
        {
            if (obj is GameVector3 otherPoint)
                return this == otherPoint;

            return false;
        }

        public float GetDistanceTo(GameVector3 target)
        {
            return (float)System.Math.Sqrt((System.Math.Pow(X - target.X, 2) + System.Math.Pow(Y - target.Y, 2) + System.Math.Pow(Z - target.Z, 2)));
        }

        public GameVector3 GetRound()
        {
            var digit = 5;
            return new GameVector3((float)System.Math.Round(X, digit), (float)System.Math.Round(Y, digit), (float)System.Math.Round(Z, digit));
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

        public static float Dot(GameVector3 lhs, GameVector3 rhs) { return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z; }

        public static GameVector3 Cross(GameVector3 lhs, GameVector3 rhs)
        {
            return new GameVector3(
                lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                lhs.Z * rhs.X - lhs.X * rhs.Z,
                lhs.X * rhs.Y - lhs.Y * rhs.X);
        }
    }
}
