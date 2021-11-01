using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    public struct FloatRect
    {
        public readonly float X { get; }
        public readonly float Y { get; }
        public readonly float Width { get; }
        public readonly float Height { get; }

        public FloatRect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float xMin => X;

        public float xMax => X + Width;

        public float yMin => Y;

        public float yMax => Y + Height;

        public bool IsInside(Point point)
        {
            return xMin <= point.X && point.X <= xMax &&
                yMin <= point.Y && point.Y <= yMax;
        }

        public bool IsInside(FloatPoint point)
        {
            return xMin <= point.X && point.X <= xMax &&
                yMin <= point.Y && point.Y <= yMax;
        }

        public FloatPoint GetRandomPoint(SessionRandom random)
        {
            return new FloatPoint(random.GetRandom(xMin, xMax), random.GetRandom(yMin, yMax));
        }
    }
}
