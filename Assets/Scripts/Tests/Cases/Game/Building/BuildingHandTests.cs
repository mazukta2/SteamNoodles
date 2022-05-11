using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class BuildingHandTests
    {
        [Test]
        public void IsHandSpawnCorrect()
        {
            var constructionDef1 = ConstructionSetups.GetDefault();
            var constructionDef2 = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.StartingHand = new List<ConstructionDefinition>() { })
                .Build();

            Assert.AreEqual(0, game.LevelCollection.FindViews<HandConstructionView>().Count);
            ICurrentLevel.Default.Hand.Add(constructionDef1);
            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count);
            ICurrentLevel.Default.Hand.Add(constructionDef2);
            Assert.AreEqual(2, game.LevelCollection.FindViews<HandConstructionView>().Count);
            ICurrentLevel.Default.Hand.Add(constructionDef1);
            Assert.AreEqual(2, game.LevelCollection.FindViews<HandConstructionView>().Count);

            ICurrentLevel.Default.Hand.Cards.First().Amount--;
            Assert.AreEqual(2, game.LevelCollection.FindViews<HandConstructionView>().Count);
            ICurrentLevel.Default.Hand.Cards.First().Amount--;
            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count);
            ICurrentLevel.Default.Hand.Cards.First().Amount--;
            Assert.AreEqual(0, game.LevelCollection.FindViews<HandConstructionView>().Count);

            game.Dispose();
        }

        [Test]
        public void IsCountersWorks()
        {
            var constructionDef1 = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.StartingHand = new List<ConstructionDefinition>() { })
                .Build();

            ICurrentLevel.Default.Hand.Add(constructionDef1);
            Assert.AreEqual(1, ICurrentLevel.Default.Hand.Cards.First().Amount);
            Assert.AreEqual("1", game.LevelCollection.FindViews<HandConstructionView>().First().Amount.Value);

            ICurrentLevel.Default.Hand.Add(constructionDef1);
            Assert.AreEqual(2, ICurrentLevel.Default.Hand.Cards.First().Amount);
            Assert.AreEqual("2", game.LevelCollection.FindViews<HandConstructionView>().First().Amount.Value);

            ICurrentLevel.Default.Hand.Cards.First().Amount--;
            Assert.AreEqual(1, ICurrentLevel.Default.Hand.Cards.First().Amount);
            Assert.AreEqual("1", game.LevelCollection.FindViews<HandConstructionView>().First().Amount.Value);

            ICurrentLevel.Default.Hand.Cards.First().Amount--;

            Assert.AreEqual(0, ICurrentLevel.Default.Hand.Cards.Count);
            Assert.AreEqual(0, game.LevelCollection.FindViews<HandConstructionView>().Count);

            game.Dispose();
        }

        //[Test]
        //public void IsCountersInBuildingWorks()
        //{
        //    var construction = ConstructionSetups.GetDefault();
        //    var game = new GameConstructor()
        //        .UpdateDefinition<LevelDefinitionMock>(x => x.StartingHand = new List<ConstructionDefinition>() { construction, construction })
        //        .Build();

        //    game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
        //    game.Controls.Click();
        //    game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
        //    game.Controls.MovePointer(new FloatPoint(-2, 0));
        //    game.Controls.Click();

        //    Assert.AreEqual(2, game.CurrentLevel.FindViews<ConstructionView>().Count());

        //    game.Dispose();
        //}

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
