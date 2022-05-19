using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Hand
{
    public class HandTests
    {
        [Test]
        public void IsFirstCardSpawned()
        {
            var construction = new ConstructionDefinition();
            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.StartingHand = new List<ConstructionDefinition>() { construction })
                .Build();

            var hand = game.LevelCollection.FindView<HandView>();
            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count());

            game.Dispose();
        }

        [Test]
        public void IsYouGetNewCardsAfterLevelUp()
        {
            var construction = ConstructionSetups.GetDefault();
            construction.Points = 2;

            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.StartingHand = new List<ConstructionDefinition>() { construction, construction })
                .Build();

            Assert.AreEqual(0, IBattleLevel.Default.Resources.Points.CurrentLevel);
            Assert.AreEqual(0, IBattleLevel.Default.Resources.Points.Value);
            Assert.AreEqual(3, IBattleLevel.Default.Resources.Points.PointsForNextLevel);

            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count());
            Assert.AreEqual("2", game.LevelCollection.FindView<HandConstructionView>().Amount.Value);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.AreEqual(0, IBattleLevel.Default.Resources.Points.CurrentLevel);
            Assert.AreEqual(2, IBattleLevel.Default.Resources.Points.Value);
            Assert.AreEqual(3, IBattleLevel.Default.Resources.Points.PointsForNextLevel);

            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count());
            Assert.AreEqual("1", game.LevelCollection.FindView<HandConstructionView>().Amount.Value);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(1, IBattleLevel.Default.Resources.Points.CurrentLevel);
            Assert.AreEqual(4, IBattleLevel.Default.Resources.Points.Value);
            Assert.AreEqual(8, IBattleLevel.Default.Resources.Points.PointsForNextLevel);

            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count());
            Assert.AreEqual("3", game.LevelCollection.FindView<HandConstructionView>().Amount.Value);

            game.Dispose();
        }

        [Test]
        public void IsIconSettedInHand()
        {
            var game = new GameConstructor()
                .Build();

            var construction = IGameDefinitions.Default.Get<ConstructionDefinition>("Construction1");

            Assert.AreEqual(construction.HandImagePath, game.LevelCollection.FindView<HandConstructionView>().Image.Path);

            game.Dispose();
        }


        [Test]
        public void IsTooltipShowing()
        {
            var game = new GameConstructor()
                .Build();

            var construction1 = game.LevelCollection.FindViews<HandConstructionView>().First();
            Assert.IsNotNull(construction1);

            Assert.IsNull(game.LevelCollection.FindView<HandConstructionTooltipView>());

            construction1.SetHighlight(true);

            Assert.IsNotNull(game.LevelCollection.FindView<HandConstructionTooltipView>());

            construction1.SetHighlight(false);

            Assert.IsNull(game.LevelCollection.FindView<HandConstructionTooltipView>());

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
