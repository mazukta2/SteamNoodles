
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System.Numerics;

namespace Game.Assets.Scripts.Game.Logic.Common.Helpers
{
    public static class MathHelpers
    {
        public static int ToMs(this float value)
        {
            return (int)(value * 1000);
        }

        public static float Lerp(float firstFloat, float secondFloat, float t)
        {
            return firstFloat * (1 - t) + secondFloat * t;
        }

        public static GameVector3 ToVector(this GameQuaternion quaternion)
        {
            return quaternion * GameVector3.Forward;
        }

        public static GameQuaternion ToQuaternion(this GameVector3 forward)
        {
            if (forward.IsZero())
                throw new System.Exception("Wrong direction");

            return GameQuaternion.LookRotation(forward);
        }


        //public static FloatPoint3D ToVector(this Quaternion quaternion)
        //{
        //    var vector = Vector3.Transform(Vector3.UnitZ, quaternion);
        //    return new FloatPoint3D(vector.X, vector.Y, vector.Z).GetRound();
        //}


        //public static Quaternion ToQuaternion(this FloatPoint3D vector)
        //{
        //    var x = new Vector3(vector.X, vector.Y, vector.Z);
        //    var n = Vector3.UnitZ;
        //    var angle = -System.Math.Acos(Vector3.Dot(x, n) / (x.Length() * n.Length()));
        //    var axis = Vector3.Normalize(Vector3.Cross(x, n));
        //    return Quaternion.CreateFromAxisAngle(axis, (float)angle);
        //}

        //public static FloatPoint3D Rotate(this FloatPoint3D vector, float yaw, float pitch, float roll)
        //{
        //    var rotation = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
        //    var currentDirection = vector.ToQuaternion() * rotation;
        //    return currentDirection.ToVector();
        //}

    }
}
