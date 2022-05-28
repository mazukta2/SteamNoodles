using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
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

            var s1 = IBattleLevel.Default.Schemes.Add(constructionDef1);
            IBattleLevel.Default.Hand.Add(s1);
            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count);
            var s2 = IBattleLevel.Default.Schemes.Add(constructionDef2);
            IBattleLevel.Default.Hand.Add(s2);
            Assert.AreEqual(2, game.LevelCollection.FindViews<HandConstructionView>().Count);
            IBattleLevel.Default.Hand.Add(s1);
            Assert.AreEqual(2, game.LevelCollection.FindViews<HandConstructionView>().Count);

            IBattleLevel.Default.Hand.Remove(IBattleLevel.Default.Hand.GetCards().First());
            Assert.AreEqual(2, game.LevelCollection.FindViews<HandConstructionView>().Count);
            IBattleLevel.Default.Hand.Remove(IBattleLevel.Default.Hand.GetCards().First());
            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count);
            IBattleLevel.Default.Hand.Remove(IBattleLevel.Default.Hand.GetCards().First());
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

            var s1 = IBattleLevel.Default.Schemes.Add(constructionDef1);
            IBattleLevel.Default.Hand.Add(s1);
            Assert.AreEqual(1, IBattleLevel.Default.Hand.GetCards().First().Amount.Value);
            Assert.AreEqual("1", game.LevelCollection.FindViews<HandConstructionView>().First().Amount.Value);

            IBattleLevel.Default.Hand.Add(s1);
            Assert.AreEqual(2, IBattleLevel.Default.Hand.GetCards().First().Amount.Value);
            Assert.AreEqual("2", game.LevelCollection.FindViews<HandConstructionView>().First().Amount.Value);

            IBattleLevel.Default.Hand.Remove(IBattleLevel.Default.Hand.GetCards().First());
            Assert.AreEqual(1, IBattleLevel.Default.Hand.GetCards().First().Amount.Value);
            Assert.AreEqual("1", game.LevelCollection.FindViews<HandConstructionView>().First().Amount.Value);

            IBattleLevel.Default.Hand.Remove(IBattleLevel.Default.Hand.GetCards().First());

            Assert.AreEqual(0, IBattleLevel.Default.Hand.GetCards().Count);
            Assert.AreEqual(0, game.LevelCollection.FindViews<HandConstructionView>().Count);

            game.Dispose();
        }


        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
