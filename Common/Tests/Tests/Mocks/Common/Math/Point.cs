using Assets.Scripts.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Mocks.Common.Math
{
    public struct Point : IPoint
    {
        public readonly int X { get; }
        public readonly int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IRect ToCenterRect()
        {
            return new Rect(-X / 2, -Y / 2, X, Y);
        }
    }
}
