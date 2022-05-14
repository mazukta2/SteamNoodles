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

        public static void SetPosition(this Transform transform, GameVector3 vector)
        {
            transform.position = new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static Vector3 ToVector(this GameVector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static GameVector3 ToVector(this Vector3 vector)
        {
            return new GameVector3(vector.x, vector.y, vector.z);
        }
    }
}
