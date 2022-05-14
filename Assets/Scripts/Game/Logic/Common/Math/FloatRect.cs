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

        public bool IsInside(GameVector3 point)
        {
            return xMin <= point.X && point.X <= xMax &&
                yMin <= point.Z && point.Z <= yMax;
        }

        public bool IsZero()
        {
            return X == 0 && Y == 0 && Height == 0 && Width == 0;
        }
    }
}
