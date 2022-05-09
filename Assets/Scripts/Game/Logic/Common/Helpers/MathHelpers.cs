
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
    }
}
