using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine.Helpers
{
    public static class MathExtentions
    {
        public static bool IsInside(this RectInt rect, Vector2Int position)
        {
            return rect.xMin <= position.x && position.x <= rect.xMax &&
                rect.yMin <= position.y && position.y <= rect.yMax;
        }

        public static void SetPosition(this Transform transform, FloatPoint3D vector)
        {
            transform.position = new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static Vector3 ToVector(this FloatPoint3D vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static FloatPoint3D ToVector(this Vector3 vector)
        {
            return new FloatPoint3D(vector.x, vector.y, vector.z);
        }
    }
}
