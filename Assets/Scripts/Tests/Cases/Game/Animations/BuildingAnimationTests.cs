using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points.BuildingPointsAnimations;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Hand
{
    public class BuildingAnimationTests
    {
        [Test]
        public void IsInstantAnimationWorks()
        {
            var definitions = new ConstructionsSettingsDefinition();

            var time = new GameTime();
            var level = new ViewsCollection();

            var curve = new BezierCurve(GameVector3.Zero, GameVector3.One, new GameVector3(1, 0, 0), new GameVector3(1, 0, 0));
            var animationModel = new AddPointsAnimation(10, definitions, time, GameVector3.Zero);
            var animation = new AddPointsAnimationPresenter(curve, CreatePieceSpawner(level), animationModel);
            animationModel.Play();
            Assert.IsTrue(animation.IsDisposed);
            Assert.AreEqual(0, level.FindViews<PieceView>().Count);

            animationModel.Dispose();
            level.Dispose();
        }

        [Test]
        public void IsAnimationDestoyed()
        {
            var definitions = new ConstructionsSettingsDefinition();
            definitions.PieceSpawningTime = 1;
            definitions.PieceMovingTime = 2;

            var time = new GameTime();
            time.MoveTime(10);
            var level = new ViewsCollection();
            var curve = new BezierCurve(GameVector3.Zero, GameVector3.One, new GameVector3(1, 0, 0), new GameVector3(1, 0, 0));

            var animationModel = new AddPointsAnimation(10, definitions, time, GameVector3.Zero);
            var animation = new AddPointsAnimationPresenter(curve, CreatePieceSpawner(level), animationModel);
            animationModel.Play();

            Assert.IsFalse(animation.IsDisposed);
            Assert.AreEqual(1, level.FindViews<PieceView>().Count);
            time.MoveTime(1);
            Assert.AreEqual(10, level.FindViews<PieceView>().Count); // everything is spawned
            time.MoveTime(1.5f);
            Assert.AreEqual(5, level.FindViews<PieceView>().Count); // half pieces should be destoyed by now
            time.MoveTime(0.5f); // everything is destoryed
            Assert.IsTrue(animation.IsDisposed);
            Assert.AreEqual(0, level.FindViews<PieceView>().Count);

            animationModel.Dispose();
            level.Dispose();
        }

        [Test]
        public void IsBezierCurveWorking()
        {
            var curve = new BezierCurve(GameVector3.Zero, GameVector3.One, GameVector3.One, GameVector3.Zero);
            Assert.AreEqual(new GameVector3(0f, 0f, 0f), curve.GetPosition(0));
            Assert.AreEqual(new GameVector3(0.5f, 0.5f, 0.5f), curve.GetPosition(0.5f));
            Assert.AreEqual(new GameVector3(1f, 1f, 1f), curve.GetPosition(1f));
        }

        [Test]
        public void IsBezierCurvePositionsWorking()
        {
            var curve = new BezierCurve(new GameVector3(1,1,1), new GameVector3(-1, 1, -1), new GameVector3(1, 3, 1), new GameVector3(-1, 3, -1));
            Assert.AreEqual(new GameVector3(0, 2.5f, 0), curve.GetPosition(0.5f));
        }

        [Test]
        public void IsPieceReachesDestination()
        {
            var definitions = new ConstructionsSettingsDefinition();
            definitions.PieceSpawningTime = 1;
            definitions.PieceMovingTime = 2;

            var time = new GameTime();
            time.MoveTime(10);
            var level = new ViewsCollection();
            var curve = new BezierCurve(GameVector3.Zero, GameVector3.One, GameVector3.One, GameVector3.Zero);
            var animationModel = new AddPointsAnimation(1, definitions, time, GameVector3.Zero);
            var animation = new AddPointsAnimationPresenter(curve, CreatePieceSpawner(level), animationModel);
            animationModel.Play();

            time.MoveTime(1);
            var piece = level.FindView<PieceView>();
            Assert.AreEqual(new GameVector3(0.5f, 0.5f, 0.5f), piece.Position.Value);
            time.MoveTime(1);
            Assert.AreEqual(new GameVector3(1f, 1f, 1f), piece.Position.Value);

            animationModel.Dispose();
            level.Dispose();
        }

        [Test]
        public void IsPiecePositionRight()
        {
            var definitions = new ConstructionsSettingsDefinition();
            definitions.PieceSpawningTime = 1;
            definitions.PieceMovingTime = 2;

            var time = new GameTime();
            time.MoveTime(10);
            var level = new ViewsCollection();
            var curve = new BezierCurve(new GameVector3(1, 1, 1), new GameVector3(-1, 1, -1), new GameVector3(1, 3, 1), new GameVector3(-1, 3, -1));

            var animationModel = new AddPointsAnimation(1, definitions, time, new GameVector3(1, 1, 1));
            var animation = new AddPointsAnimationPresenter(curve, CreatePieceSpawner(level), animationModel);
            animationModel.Play();

            time.MoveTime(1);
            var piece = level.FindView<PieceView>();
            Assert.AreEqual(new GameVector3(0, 2.5f, 0), piece.Position.Value);

            animationModel.Dispose();
            level.Dispose();
        }

        private PointPieceSpawnerPresenter CreatePieceSpawner(ViewsCollection level)
        {
            var view = new PieceSpawnerView(level);
            return new PointPieceSpawnerPresenter(view);
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
