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

        public static Vector3 ToUnityVector(this FloatPoint vector, float y)
        {
            return new Vector3(vector.X, 0, vector.Y);
        }

        public static FloatPoint ToLogicVector(this Vector3 vector)
        {
            return new FloatPoint(vector.x, vector.z);
        }
    }
}
