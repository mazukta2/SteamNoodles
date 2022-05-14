using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Numerics;
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

        public static GameQuaternion ToDirection(FieldRotation rotation)
        {
            if (rotation == FieldRotation.Top)
                return new FloatPoint3D(0, 0, 1).ToQuaternion();
            if (rotation == FieldRotation.Right)
                return new FloatPoint3D(1, 0, 0).ToQuaternion();
            if (rotation == FieldRotation.Bottom)
                return new FloatPoint3D(0, 0, -1).ToQuaternion();

            return new FloatPoint3D(-1, 0, 0).ToQuaternion();
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
