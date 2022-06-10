using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions
{
    public record FieldRotation
    {
        public static FieldRotation Default { get; } = new FieldRotation();

        public FieldRotation()
        {
            Value = Rotation.Top;
        }

        public FieldRotation(Rotation value)
        {
            Value = value;
        }

        public Rotation Value { get; }

        public static FieldRotation RotateLeft(FieldRotation rotation)
        {
            if (rotation.Value == Rotation.Top)
                return new (Rotation.Left);
            if (rotation.Value == Rotation.Left)
                return new (Rotation.Bottom);
            if (rotation.Value == Rotation.Bottom)
                return new(Rotation.Right);
            return new(Rotation.Top);
        }

        public static FieldRotation RotateRight(FieldRotation rotation)
        {
            if (rotation.Value == Rotation.Top)
                return new(Rotation.Right);
            if (rotation.Value == Rotation.Right)
                return new(Rotation.Bottom);
            if (rotation.Value == Rotation.Bottom)
                return new(Rotation.Left);
            return new(Rotation.Top);
        }

        public static GameQuaternion ToDirection(FieldRotation rotation)
        {
            if (rotation.Value == Rotation.Top)
                return new GameVector3(0, 0, 1).ToQuaternion();
            if (rotation.Value == Rotation.Right)
                return new GameVector3(1, 0, 0).ToQuaternion();
            if (rotation.Value == Rotation.Bottom)
                return new GameVector3(0, 0, -1).ToQuaternion();

            return new GameVector3(-1, 0, 0).ToQuaternion();
        }

        public enum Rotation
        {
            Top,
            Left,
            Right,
            Bottom
        }

        internal static GameQuaternion ToDirection(object rotation)
        {
            throw new NotImplementedException();
        }
    }
}
