using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class WavesTests
    {
        [Test]
        public void IsEndWaveButtonRemovesBuildings()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition, constructionDefinition })
                .Build();

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();
            Assert.AreEqual(1, game.CurrentLevel.FindViews<ConstructionView>().Count);
            var firstBuilding = game.CurrentLevel.FindView<ConstructionView>();

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new FloatPoint(-1, 0));
            game.Controls.Click();
            Assert.AreEqual(2, game.CurrentLevel.FindViews<ConstructionView>().Count);

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new FloatPoint(-2, 0));
            game.Controls.Click();
            Assert.AreEqual(3, game.CurrentLevel.FindViews<ConstructionView>().Count);

            game.CurrentLevel.FindView<MainScreenView>().NextWaveButton.Click();

            Assert.AreEqual(1, game.CurrentLevel.FindViews<ConstructionView>().Count);
            Assert.AreEqual(firstBuilding, game.CurrentLevel.FindView<ConstructionView>());

            game.Dispose();
        }


        [Test]
        public void IsEndWaveButtonProgress()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition, constructionDefinition })
                .UpdateDefinition<LevelDefinitionMock>(x => x.ConstructionsForNextWave = 2)
                .Build();

            Assert.AreEqual(0, game.CurrentLevel.FindView<MainScreenView>().NextWaveProgress.Value);
            Assert.IsFalse(game.CurrentLevel.FindView<MainScreenView>().NextWaveButton.IsActive);
            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.AreEqual(0.5f, game.CurrentLevel.FindView<MainScreenView>().NextWaveProgress.Value);
            Assert.IsFalse(game.CurrentLevel.FindView<MainScreenView>().NextWaveButton.IsActive);

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new FloatPoint(-1, 0));
            game.Controls.Click();

            Assert.AreEqual(1f, game.CurrentLevel.FindView<MainScreenView>().NextWaveProgress.Value);
            Assert.IsTrue(game.CurrentLevel.FindView<MainScreenView>().NextWaveButton.IsActive);

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new FloatPoint(-2, 0));
            game.Controls.Click();

            Assert.AreEqual(1f, game.CurrentLevel.FindView<MainScreenView>().NextWaveProgress.Value);
            Assert.IsTrue(game.CurrentLevel.FindView<MainScreenView>().NextWaveButton.IsActive);

            game.Dispose();
        }

        [Test]
        public void IsEndWaveButtonGiveYouNewBuildings()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition })
                .Build();

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.IsTrue(game.CurrentLevel.FindView<MainScreenView>().NextWaveButton.IsActive);
            game.CurrentLevel.FindView<MainScreenView>().NextWaveButton.Click();

            Assert.AreEqual(3, game.CurrentLevel.FindViews<HandConstructionView>().Count());

            game.Dispose();
        }


        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
