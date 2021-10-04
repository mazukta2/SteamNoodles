using System;

namespace Assets.Scripts.Game.Logic.Common.Math
{
    public static class GameMath
    {
        public static float Clamp(float value, float min, float max)
        {
            return System.Math.Max(min, System.Math.Min(max, value));
        }
    }
}
