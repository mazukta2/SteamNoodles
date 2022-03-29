using Game.Assets.Scripts.Game.Logic.Models.Session;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    public struct FloatRect
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

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

        public bool IsInside(FloatPoint point)
        {
            return xMin <= point.X && point.X <= xMax &&
                yMin <= point.Y && point.Y <= yMax;
        }

        public FloatPoint GetRandomPoint(SessionRandom random)
        {
            return new FloatPoint(random.GetRandom(xMin, xMax), random.GetRandom(yMin, yMax));
        }

        public bool IsZero()
        {
            return X == 0 && Y == 0 && Height == 0 && Width == 0;
        }
    }
}
