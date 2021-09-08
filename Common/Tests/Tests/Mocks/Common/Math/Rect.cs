using Assets.Scripts.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Mocks.Common.Math
{
    public struct Rect : IRect
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Width;
        public readonly int Height;

        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int xMin => X;

        public int xMax => X + Width;

        public int yMin => Y;

        public int yMax => Y + Height;

        public bool IsInside(IPoint point)
        {
            return (xMin <= point.X && point.X <= xMax &&
                yMin <= point.Y && point.Y <= yMax);
        }
    }
}
