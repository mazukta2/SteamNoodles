using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class ConstructionRotation
    {
        public static FieldRotation RotateLeft(FieldRotation rotation)
        {
            if (rotation == FieldRotation.Top)
                return FieldRotation.Left;
            if (rotation == FieldRotation.Left)
                return FieldRotation.Bottom;
            if (rotation == FieldRotation.Bottom)
                return FieldRotation.Right;
            return FieldRotation.Top;
        }

        public static FieldRotation RotateRight(FieldRotation rotation)
        {
            if (rotation == FieldRotation.Top)
                return FieldRotation.Right;
            if (rotation == FieldRotation.Right)
                return FieldRotation.Bottom;
            if (rotation == FieldRotation.Bottom)
                return FieldRotation.Left;
            return FieldRotation.Top;
        }

        public static FloatPoint ToDirection(FieldRotation rotation)
        {
            if (rotation == FieldRotation.Top)
                return new FloatPoint(0, 1);
            if (rotation == FieldRotation.Right)
                return new FloatPoint(1, 0);
            if (rotation == FieldRotation.Bottom)
                return new FloatPoint(0, -1);

            return new FloatPoint(-1, 0);
        }
    }

    public enum FieldRotation
    {
        Top,
        Left,
        Right,
        Bottom
    }
}
