using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class ConstructionAdjacencyTests
    {
        [Test]
        public void IsPointsForBuildingWorking()
        {
            var game = new GameConstructor()
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 5)
                .Build();

            Assert.IsNotNull(game.CurrentLevel.FindView<MainScreenView>());
            Assert.AreEqual("0/3", game.CurrentLevel.FindView<MainScreenView>().Points.Value);

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();

            Assert.AreEqual("+5", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

            game.Controls.Click();

            Assert.AreEqual("5/8", game.CurrentLevel.FindView<MainScreenView>().Points.Value);

            game.Dispose();
        }

        [Test]
        public void IsNotGetPointsForBuildingOutsideField()
        {
            var game = new GameConstructor()
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 5)
                .Build();

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();

            Assert.AreEqual("+5", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new Scripts.Game.Logic.Common.Math.FloatPoint(999, 999));

            Assert.AreEqual("0", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

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

            var game = new GameConstructor()
                .AddDefinition("construction1", construction1)
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .UpdateDefinition<LevelDefinitionMock>(x => x.ConstructionsReward = new Dictionary<ConstructionDefinition, int>())
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { construction1, construction1 })
                .Build();

            Assert.AreEqual("0/3", game.CurrentLevel.FindView<MainScreenView>().Points.Value);
            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("+5", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);
            game.Controls.Click();
            Assert.AreEqual(1, game.CurrentLevel.FindViews<ConstructionView>().Count);
            Assert.AreEqual("5/8", game.CurrentLevel.FindView<MainScreenView>().Points.Value);

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("0", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new FloatPoint(-2, 0));
            Assert.AreEqual("+7", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

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
            var game = new GameConstructor()
                .AddDefinition("construction1", constructionDefinition)
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .UpdateDefinition<LevelDefinitionMock>(x => x.ConstructionsReward = new Dictionary<ConstructionDefinition, int>())
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition })
                .Build();

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("0", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new FloatPoint(1, 0));
            Assert.AreEqual("0", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new FloatPoint(2, 0));
            Assert.AreEqual("0", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new FloatPoint(3, 0));
            Assert.AreEqual("+7", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);


            game.Controls.MovePointer(new FloatPoint(-1, 0));
            Assert.AreEqual("0", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new FloatPoint(-2, 0));
            Assert.AreEqual("0", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new FloatPoint(-3, 0));
            Assert.AreEqual("+7", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new FloatPoint(0, -1));
            Assert.AreEqual("+7", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new FloatPoint(0, 1));
            Assert.AreEqual("+7", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
