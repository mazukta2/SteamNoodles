using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
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

            game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveButton.Click();

            Assert.AreEqual(1, game.LevelCollection.FindViews<ConstructionView>().Count);
            Assert.AreEqual(firstBuilding, game.LevelCollection.FindView<ConstructionView>());

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
            Assert.IsTrue(game.LevelCollection.FindView<WaveWidgetViewMock>().FailWaveButton.IsActive);
            game.LevelCollection.FindView<WaveWidgetViewMock>().FailWaveButton.Click();

            Assert.AreEqual(1, game.LevelCollection.FindViews<ConstructionView>().Count);
            Assert.AreEqual(firstBuilding, game.LevelCollection.FindView<ConstructionView>());

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

            Assert.AreEqual(0, game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveProgress.Value);
            Assert.IsFalse(game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveButton.IsActive);
            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.AreEqual(0.5f, game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveProgress.Value);
            Assert.IsFalse(game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveButton.IsActive);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(1f, game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveProgress.Value);
            Assert.IsTrue(game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveButton.IsActive);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(1f, game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveProgress.Value);
            Assert.IsTrue(game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveButton.IsActive);

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

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.IsTrue(game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveButton.IsActive);
            game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveButton.Click();

            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count());
            Assert.AreEqual("3", game.LevelCollection.FindView<HandConstructionView>().Amount.Value);

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

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.IsTrue(game.LevelCollection.FindView<WaveWidgetViewMock>().FailWaveButton.IsActive);
            game.LevelCollection.FindView<WaveWidgetViewMock>().FailWaveButton.Click();

            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count());
            Assert.AreEqual("3", game.LevelCollection.FindView<HandConstructionView>().Amount.Value);

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

            Assert.IsFalse(game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveButton.IsActive);
            Assert.IsFalse(game.LevelCollection.FindView<WaveWidgetViewMock>().FailWaveButton.IsActive);
            Assert.AreEqual(WaveWidgetPresenter.WaveButtonAnimations.None.ToString(), game.LevelCollection.FindView<WaveWidgetViewMock>().WaveButtonAnimator.Animation);
            
            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.IsFalse(game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveButton.IsActive);
            Assert.IsFalse(game.LevelCollection.FindView<WaveWidgetViewMock>().FailWaveButton.IsActive);
            Assert.AreEqual(WaveWidgetPresenter.WaveButtonAnimations.NextWave.ToString(), game.LevelCollection.FindView<WaveWidgetViewMock>().WaveButtonAnimator.Animation);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(0, game.LevelCollection.FindViews<HandConstructionView>().Count);
            Assert.IsFalse(game.LevelCollection.FindView<WaveWidgetViewMock>().NextWaveButton.IsActive);
            Assert.IsTrue(game.LevelCollection.FindView<WaveWidgetViewMock>().FailWaveButton.IsActive);
            Assert.AreEqual(WaveWidgetPresenter.WaveButtonAnimations.FailWave.ToString(), game.LevelCollection.FindView<WaveWidgetViewMock>().WaveButtonAnimator.Animation);

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
