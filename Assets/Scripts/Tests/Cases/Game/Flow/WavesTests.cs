using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Tests.Definitions;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Flow
{
    public class WavesTests
    {
        [Test]
        public void IsEndWaveButtonRemovesBuildings()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition, constructionDefinition })
                .Build();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();
            Assert.AreEqual(1, game.Views.FindViews<ConstructionView>().Count);
            var firstBuilding = game.Views.FindView<ConstructionView>();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            game.Controls.Click();
            Assert.AreEqual(2, game.Views.FindViews<ConstructionView>().Count);

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            game.Controls.Click();
            Assert.AreEqual(3, game.Views.FindViews<ConstructionView>().Count);

            game.Views.FindView<WaveWidgetView>().NextWaveButton.Click();

            Assert.AreEqual(1, game.Views.FindViews<ConstructionView>().Count);
            Assert.AreEqual(firstBuilding, game.Views.FindView<ConstructionView>());

            game.Dispose();
        }

        [Test]
        public void IsFailWaveButtonRemovesBuildings()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new BuildConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>(x => x.LevelUpOffset = 10)
                .UpdateDefinition<ConstructionsSettingsDefinition>(x => x.LevelUpPower = 10)
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.ConstructionsForNextWave = 5)
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition, constructionDefinition })
                .Build();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();
            Assert.AreEqual(1, game.Views.FindViews<ConstructionView>().Count);
            var firstBuilding = game.Views.FindView<ConstructionView>();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            game.Controls.Click();
            Assert.AreEqual(2, game.Views.FindViews<ConstructionView>().Count);

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            game.Controls.Click();
            Assert.AreEqual(3, game.Views.FindViews<ConstructionView>().Count);

            Assert.AreEqual(0, game.Views.FindViews<HandConstructionView>().Count);
            Assert.IsTrue(game.Views.FindView<WaveWidgetView>().FailWaveButton.IsActive);
            game.Views.FindView<WaveWidgetView>().FailWaveButton.Click();

            Assert.AreEqual(1, game.Views.FindViews<ConstructionView>().Count);
            Assert.AreEqual(firstBuilding, game.Views.FindView<ConstructionView>());

            game.Dispose();
        }

        [Test]
        public void IsEndWaveButtonProgress()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition, constructionDefinition })
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.ConstructionsForNextWave = 2)
                .Build();

            Assert.AreEqual(0, game.Views.FindView<WaveWidgetView>().NextWaveProgress.Value);
            Assert.IsFalse(game.Views.FindView<WaveWidgetView>().NextWaveButton.IsActive);
            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.AreEqual(0.5f, game.Views.FindView<WaveWidgetView>().NextWaveProgress.Value);
            Assert.IsFalse(game.Views.FindView<WaveWidgetView>().NextWaveButton.IsActive);

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(1f, game.Views.FindView<WaveWidgetView>().NextWaveProgress.Value);
            Assert.IsTrue(game.Views.FindView<WaveWidgetView>().NextWaveButton.IsActive);

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(1f, game.Views.FindView<WaveWidgetView>().NextWaveProgress.Value);
            Assert.IsTrue(game.Views.FindView<WaveWidgetView>().NextWaveButton.IsActive);

            game.Dispose();
        }

        [Test]
        public void IsEndWaveButtonGiveYouNewBuildings()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition })
                .Build();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.IsTrue(game.Views.FindView<WaveWidgetView>().NextWaveButton.IsActive);
            game.Views.FindView<WaveWidgetView>().NextWaveButton.Click();

            Assert.AreEqual(1, game.Views.FindViews<HandConstructionView>().Count());
            Assert.AreEqual("3", game.Views.FindView<HandConstructionView>().Amount.Value);

            game.Dispose();
        }

        [Test]
        public void IsFailWaveButtonGiveYouNewBuildings()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.ConstructionsForNextWave = 5)
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition })
                .Build();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.IsTrue(game.Views.FindView<WaveWidgetView>().FailWaveButton.IsActive);
            game.Views.FindView<WaveWidgetView>().FailWaveButton.Click();

            Assert.AreEqual(1, game.Views.FindViews<HandConstructionView>().Count());
            Assert.AreEqual("3", game.Views.FindView<HandConstructionView>().Amount.Value);

            game.Dispose();
        }


        [Test]
        public void IsFailButtonActivating()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.ConstructionsForNextWave = 3)
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition })
                .Build();


            //Assert.IsFalse(game.Views.FindView<WaveWidgetView>().NextWaveButton.IsActive);
           // Assert.IsFalse(game.Views.FindView<WaveWidgetView>().FailWaveButton.IsActive);
           // Assert.AreEqual(WaveWidgetPresenter.WaveButtonAnimations.Hidden.ToString(), game.Views.FindView<WaveWidgetView>().WaveButtonAnimator.Animation);
            
            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.IsFalse(game.Views.FindView<WaveWidgetView>().NextWaveButton.IsActive);
            Assert.IsFalse(game.Views.FindView<WaveWidgetView>().FailWaveButton.IsActive);
            Assert.AreEqual(WaveWidgetPresenter.WaveButtonAnimations.NextWave.ToString(), game.Views.FindView<WaveWidgetView>().WaveButtonAnimator.Animation);

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(0, game.Views.FindViews<HandConstructionView>().Count);
            Assert.IsFalse(game.Views.FindView<WaveWidgetView>().NextWaveButton.IsActive);
            Assert.IsTrue(game.Views.FindView<WaveWidgetView>().FailWaveButton.IsActive);
            Assert.AreEqual(WaveWidgetPresenter.WaveButtonAnimations.FailWave.ToString(), game.Views.FindView<WaveWidgetView>().WaveButtonAnimator.Animation);

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
