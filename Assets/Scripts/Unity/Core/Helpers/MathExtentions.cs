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
    }
}
