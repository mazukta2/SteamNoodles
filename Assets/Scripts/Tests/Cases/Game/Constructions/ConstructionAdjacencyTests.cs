using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Tests.Definitions;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions
{
    public class ConstructionAdjacencyTests
    {
        [Test]
        public void IsPointsForBuildingWorking()
        {
            var game = new BuildConstructor()
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 5)
                .Build();

            Assert.IsNotNull(game.LevelCollection.FindView<PointCounterWidgetView>());
            Assert.AreEqual("0/3", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();

            Assert.AreEqual("+5", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Controls.Click();

            Assert.AreEqual("5/8", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);

            game.Dispose();
        }

        [Test]
        public void IsNotGetPointsForBuildingOutsideField()
        {
            var game = new BuildConstructor()
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 5)
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();

            Assert.AreEqual("+5", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new Scripts.Game.Logic.Common.Math.GameVector3(999, 0, 999));

            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Dispose();
        }

        [Test]
        public void IsPointsChangedByAdjacency()
        {
            var construction1 = new ConstructionDefinition()
            {
                Points = 5,
                Placement = new int[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                },
                LevelViewPath = "DebugConstruction",
            };
            construction1.AdjacencyPoints = new Dictionary<ConstructionDefinition, int>() { { construction1, 2 } };

            var game = new BuildConstructor()
                .AddDefinition("construction1", construction1)
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.ConstructionsReward = new Dictionary<ConstructionDefinition, int>())
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.
                    StartingHand = new List<ConstructionDefinition>() { construction1, construction1 })
                .Build();

            Assert.AreEqual("0/3", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);
            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("+5", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.Click();
            Assert.AreEqual(1, game.LevelCollection.FindViews<ConstructionView>().Count);
            Assert.AreEqual("5/8", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            Assert.AreEqual("+7", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Dispose();
        }

        [Test]
        public void IsNotGetPointsForBuildingInWrongPlace()
        {
            var constructionDefinition = new ConstructionDefinition()
            {
                Points = 5,
                Placement = new int[,] {
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                },
                LevelViewPath = "DebugConstruction",
            };
            constructionDefinition.AdjacencyPoints = new Dictionary<ConstructionDefinition, int>() { { constructionDefinition, 2 } };
            var game = new BuildConstructor()
                .AddDefinition("construction1", constructionDefinition)
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.ConstructionsReward = new Dictionary<ConstructionDefinition, int>())
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition })
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new GameVector3(1, 0, 0));
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new GameVector3(2, 0, 0));
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new GameVector3(3, 0, 0));
            Assert.AreEqual("+7", game.LevelCollection.FindView<BuildScreenView>().Points.Value);


            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new GameVector3(-3, 0, 0));
            Assert.AreEqual("+7", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new GameVector3(0, 0, -1));
            Assert.AreEqual("+7", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new GameVector3(0, 0, 1));
            Assert.AreEqual("+7", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
