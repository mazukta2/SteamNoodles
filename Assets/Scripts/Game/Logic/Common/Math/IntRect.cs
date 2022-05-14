using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    public struct IntRect
    {
        public readonly int X { get; }
        public readonly int Y { get; }
        public readonly int Width { get; }
        public readonly int Height { get; }

        public IntRect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int xMin => X;

        public int xMax => X + Width - 1;

        public int yMin => Y;

        public int yMax => Y + Height -1;

        public bool IsInside(IntPoint point)
        {
            return xMin <= point.X && point.X <= xMax &&
                yMin <= point.Y && point.Y <= yMax;
        }

        public bool IsInside(GameVector3 point)
        {
            return xMin <= point.X && point.X <= xMax &&
                yMin <= point.Z && point.Z <= yMax;
        }

        public bool IsHorisontalyInside(GameVector3 point)
        {
            return xMin <= point.X && point.X <= xMax;
        }

        public IntPoint GetRandomPoint(SessionRandom random)
        {
            return new IntPoint(random.GetRandom(xMin, xMax), random.GetRandom(yMin, yMax));
        }

        public static FloatRect operator *(IntRect current, float other) => new FloatRect(current.X * other, current.Y * other, current.Width * other, current.Height * other);

        public override bool Equals(object obj)
        {
            if (obj is IntRect otherPoint)
                return this.X == otherPoint.X && this.Y == otherPoint.Y && this.Height == otherPoint.Height && this.Width == otherPoint.Width;

            return false;
        }

        public override int GetHashCode()
        {
            return X + Y;
        }

        public override string ToString()
        {
            return $"[{X}, {Y}, {Width}, {Height}]";
        }
    }
}
