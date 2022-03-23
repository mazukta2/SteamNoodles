using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
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
            game.Engine.Controls.Click();

            Assert.AreEqual("5", game.CurrentLevel.FindView<MainScreenView>().Points.Value);

            game.Dispose();
        }

        [Test]
        public void IsPointsChangedByAdjacency()
        {
            var game = new GameTestConstructor()
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 5)
                .Build();

            //Assert.AreEqual(0, game.CurrentLevel.FindView<MainScreenView>().Points);
            //game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();
            //game.Engine.Controls.Click();

            //Assert.AreEqual(5, game.CurrentLevel.FindView<MainScreenView>().Points);

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
