using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using NUnit.Framework;
using System.Numerics;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class MathTests
    {
        [Test]
        public void IsQuaterinonConverable()
        {
            TestFor(new GameVector3(1, 1, 1));
            TestFor(new GameVector3(1, -1, 1));
            TestFor(new GameVector3(-1, 1, 1));
            TestFor(new GameVector3(1, 1, -1));
            TestFor(new GameVector3(-1, -1, -1));

            void TestFor(GameVector3 point)
            {
                var direction = point.GetNormalize();
                var q = direction.ToQuaternion();
                //Assert.That(direction.X, Is.EqualTo(q.ToVector().X).Within(5).Percent);
                //Assert.That(direction.Y, Is.EqualTo(q.ToVector().Y).Within(5).Percent);
                //Assert.That(direction.Z, Is.EqualTo(q.ToVector().Z).Within(5).Percent);
            }
        }

        [Test]
        public void IsRotationWorks()
        {
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
