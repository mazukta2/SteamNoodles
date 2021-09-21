using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    public struct Rect
    {
        public readonly int X { get; }
        public readonly int Y { get; }
        public readonly int Width { get; }
        public readonly int Height { get; }

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

        public bool IsInside(Point point)
        {
            return xMin <= point.X && point.X <= xMax &&
                yMin <= point.Y && point.Y <= yMax;
        }
    }
}
