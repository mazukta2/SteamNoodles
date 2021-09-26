using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Helpers
{
    public static class MathExtentions
    {
        public static bool IsInside(this RectInt rect, Vector2Int position)
        {
            return rect.xMin <= position.x && position.x <= rect.xMax &&
                rect.yMin <= position.y && position.y <= rect.yMax;
        }

        public static Vector2 ToUnityVector(this System.Numerics.Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector3 ToUnityVector(this System.Numerics.Vector2 vector, float z)
        {
            return new Vector3(vector.X, vector.Y, z);
        }

        public static System.Numerics.Vector2 ToLogicVector(this Vector2 vector)
        {
            return new System.Numerics.Vector2(vector.x, vector.y);
        }

        public static System.Numerics.Vector2 ToLogicVector(this Vector3 vector)
        {
            return new System.Numerics.Vector2(vector.x, vector.y);
        }
    }
}
