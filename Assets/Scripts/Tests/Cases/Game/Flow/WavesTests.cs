using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
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
                .AddDefinition("d", constructionDefinition)
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition, constructionDefinition })
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();
            Assert.AreEqual(1, game.LevelCollection.FindViews<ConstructionView>().Count);
            var firstBuilding = game.LevelCollection.FindView<ConstructionView>();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            game.Controls.Click();
            Assert.AreEqual(2, game.LevelCollection.FindViews<ConstructionView>().Count);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            game.Controls.Click();
            Assert.AreEqual(3, game.LevelCollection.FindViews<ConstructionView>().Count);

            game.LevelCollection.FindView<MainScreenView>().NextWaveButton.Click();

            Assert.AreEqual(1, game.LevelCollection.FindViews<ConstructionView>().Count);
            Assert.AreEqual(firstBuilding, game.LevelCollection.FindView<ConstructionView>());

            game.Dispose();
        }

        [Test]
        public void IsFailWaveButtonRemovesBuildings()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>(x => x.LevelUpOffset = 10)
                .UpdateDefinition<ConstructionsSettingsDefinition>(x => x.LevelUpPower = 10)
                .UpdateDefinition<LevelDefinitionMock>(x => x.ConstructionsForNextWave = 5)
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition, constructionDefinition })
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();
            Assert.AreEqual(1, game.LevelCollection.FindViews<ConstructionView>().Count);
            var firstBuilding = game.LevelCollection.FindView<ConstructionView>();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            game.Controls.Click();
            Assert.AreEqual(2, game.LevelCollection.FindViews<ConstructionView>().Count);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            game.Controls.Click();
            Assert.AreEqual(3, game.LevelCollection.FindViews<ConstructionView>().Count);

            Assert.AreEqual(0, game.LevelCollection.FindViews<HandConstructionView>().Count);
            Assert.IsTrue(game.LevelCollection.FindView<MainScreenView>().FailWaveButton.IsActive);
            game.LevelCollection.FindView<MainScreenView>().FailWaveButton.Click();

            Assert.AreEqual(1, game.LevelCollection.FindViews<ConstructionView>().Count);
            Assert.AreEqual(firstBuilding, game.LevelCollection.FindView<ConstructionView>());

            game.Dispose();
        }

        [Test]
        public void IsEndWaveButtonProgress()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .AddDefinition("d", constructionDefinition)
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition, constructionDefinition })
                .UpdateDefinition<LevelDefinitionMock>(x => x.ConstructionsForNextWave = 2)
                .Build();

            Assert.AreEqual(0, game.LevelCollection.FindView<MainScreenView>().NextWaveProgress.Value);
            Assert.IsFalse(game.LevelCollection.FindView<MainScreenView>().NextWaveButton.IsActive);
            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.AreEqual(0.5f, game.LevelCollection.FindView<MainScreenView>().NextWaveProgress.Value);
            Assert.IsFalse(game.LevelCollection.FindView<MainScreenView>().NextWaveButton.IsActive);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(1f, game.LevelCollection.FindView<MainScreenView>().NextWaveProgress.Value);
            Assert.IsTrue(game.LevelCollection.FindView<MainScreenView>().NextWaveButton.IsActive);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(1f, game.LevelCollection.FindView<MainScreenView>().NextWaveProgress.Value);
            Assert.IsTrue(game.LevelCollection.FindView<MainScreenView>().NextWaveButton.IsActive);

            game.Dispose();
        }

        [Test]
        public void IsEndWaveButtonGiveYouNewBuildings()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .AddDefinition("d", constructionDefinition)
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition })
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.IsTrue(game.LevelCollection.FindView<MainScreenView>().NextWaveButton.IsActive);
            game.LevelCollection.FindView<MainScreenView>().NextWaveButton.Click();

            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count());
            Assert.AreEqual("3", game.LevelCollection.FindView<HandConstructionView>().Amount.Value);

            game.Dispose();
        }

        [Test]
        public void IsFailWaveButtonGiveYouNewBuildings()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.ConstructionsForNextWave = 5)
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition })
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.IsTrue(game.LevelCollection.FindView<MainScreenView>().FailWaveButton.IsActive);
            game.LevelCollection.FindView<MainScreenView>().FailWaveButton.Click();

            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count());
            Assert.AreEqual("3", game.LevelCollection.FindView<HandConstructionView>().Amount.Value);

            game.Dispose();
        }


        [Test]
        public void IsFailButtonActivating()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.ConstructionsForNextWave = 3)
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition })
                .Build();

            Assert.IsFalse(game.LevelCollection.FindView<MainScreenView>().NextWaveButton.IsActive);
            Assert.IsFalse(game.LevelCollection.FindView<MainScreenView>().FailWaveButton.IsActive);
            Assert.AreEqual(MainScreenPresenter.WaveButtonAnimations.None.ToString(), game.LevelCollection.FindView<MainScreenView>().WaveButtonAnimator.Animation);
            
            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.IsFalse(game.LevelCollection.FindView<MainScreenView>().NextWaveButton.IsActive);
            Assert.IsFalse(game.LevelCollection.FindView<MainScreenView>().FailWaveButton.IsActive);
            Assert.AreEqual(MainScreenPresenter.WaveButtonAnimations.NextWave.ToString(), game.LevelCollection.FindView<MainScreenView>().WaveButtonAnimator.Animation);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(0, game.LevelCollection.FindViews<HandConstructionView>().Count);
            Assert.IsFalse(game.LevelCollection.FindView<MainScreenView>().NextWaveButton.IsActive);
            Assert.IsTrue(game.LevelCollection.FindView<MainScreenView>().FailWaveButton.IsActive);
            Assert.AreEqual(MainScreenPresenter.WaveButtonAnimations.FailWave.ToString(), game.LevelCollection.FindView<MainScreenView>().WaveButtonAnimator.Animation);

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
