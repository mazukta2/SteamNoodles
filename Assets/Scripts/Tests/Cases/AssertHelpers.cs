using Game.Assets.Scripts.Game.Logic.Common.Math;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases
{
    public static class AssertHelpers
    {
        public static void CompareVectors(GameVector3 vector1, GameVector3 vector2, float offset = 0.0005f)
        {
            Assert.That(vector1.X, Is.EqualTo(vector2.X).Within(offset));
            Assert.That(vector1.Y, Is.EqualTo(vector2.Y).Within(offset));
            Assert.That(vector1.Z, Is.EqualTo(vector2.Z).Within(offset));
        }
    }

    public static class TestOrders
    {
        public const int Essential = 1;
        public const int Models = 2;
        public const int Presenters = 3;
    }
}
