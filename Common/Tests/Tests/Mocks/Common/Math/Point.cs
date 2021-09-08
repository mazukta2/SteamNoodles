using Assets.Scripts.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Mocks.Common.Math
{
    public struct Point : IPoint
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
