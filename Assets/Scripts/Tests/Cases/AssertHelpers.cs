using Game.Assets.Scripts.Game.Logic.Common.Math;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Cases
{
    public static class AssertHelpers
    {
        public static void CompareVectors(FloatPoint3D vector1, FloatPoint3D vector2, float offset = 0.0005f)
        {
            Assert.That(vector1.X, Is.EqualTo(vector2.X).Within(offset));
            Assert.That(vector1.Y, Is.EqualTo(vector2.Y).Within(offset));
            Assert.That(vector1.Z, Is.EqualTo(vector2.Z).Within(offset));
        }
    }
}
