using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Managers.Game;
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
            var game = new GameTestConstructor()
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 5)
                .Build();

            Assert.IsNotNull(game.CurrentLevel.FindView<MainScreenView>());
            Assert.AreEqual("0", game.CurrentLevel.FindView<MainScreenView>().Points.Value);

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();

            Assert.AreEqual("+5", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

            game.Engine.Controls.Click();

            Assert.AreEqual("4", game.CurrentLevel.FindView<MainScreenView>().Points.Value);

            game.Dispose();
        }

        [Test]
        public void IsNotGetPointsForBuildingOutsideField()
        {
            var game = new GameTestConstructor()
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 5)
                .Build();

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();

            Assert.AreEqual("+5", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

            game.Engine.Controls.MovePointer(new Scripts.Game.Logic.Common.Math.FloatPoint(999, 999));

            Assert.AreEqual("+0", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);

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

            var game = new GameTestConstructor()
                .AddDefinition("construction1", construction1)
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .UpdateDefinition<CustomerDefinition>(x => x.ConstructionsReward = new Dictionary<ConstructionDefinition, int>())
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { construction1, construction1 })
                .Build();

            Assert.AreEqual("0", game.CurrentLevel.FindView<MainScreenView>().Points.Value);
            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("+5", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);
            game.Engine.Controls.Click();
            Assert.AreEqual("4", game.CurrentLevel.FindView<MainScreenView>().Points.Value); // -1 becouse turn tick

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("+0", game.CurrentLevel.FindView<BuildScreenView>().Points.Value);
            game.Engine.Controls.MovePointer(new FloatPoint(-2, 0));
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
