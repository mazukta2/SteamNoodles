using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Tests.Environment.Common;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using Game.Assets.Scripts.Tests.Environment.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.External;

namespace Game.Assets.Scripts.Tests.Cases.Game.Hand
{
    public class HandTests
    {
        [Test]
        public void IsFirstCardSpawned()
        {
            var construction = new ConstructionDefinition();
            var game = new GameTestConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.StartingHand = new List<ConstructionDefinition>() { construction })
                .Build();

            var hand = game.CurrentLevel.FindView<HandView>();
            Assert.AreEqual(1, game.CurrentLevel.FindViews<HandConstructionView>().Count());

            game.Dispose();
        }

        [Test]
        public void IsYouGetNewCardsAfterBuilding()
        {
            var game = new GameTestConstructor()
                .Build();

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Engine.Controls.Click();

            Assert.AreEqual(1, game.CurrentLevel.FindViews<HandConstructionView>().Count());

            game.Dispose();
        }

        [Test]
        public void IsIconSettedInHand()
        {
            var game = new GameTestConstructor()
                .Build();

            var construction = IDefinitions.Default.Get<ConstructionDefinition>("Construction1");

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Engine.Controls.Click();

            Assert.AreEqual(construction.HandImagePath, game.CurrentLevel.FindView<HandConstructionView>().Image.Path);

            game.Dispose();
        }

        [Test]
        public void IsHandLimitWorking()
        {
            //var construction1 = new ConstructionDefinition();
            //var construction2 = new ConstructionDefinition();

            //var game = new GameTestConstructor()
            //    .UpdateDefinition<CustomerDefinition>(x => x.ConstrcutionsReward = new Dictionary<ConstructionDefinition, int>() {
            //        {construction1,  1},
            //        {construction2,  1},
            //    })
            //    .Build();

            //game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();
            //game.Engine.Controls.Click();
            //Assert.AreEqual(2, game.CurrentLevel.FindViews<HandConstructionView>().Count());
            //game.Dispose();
        }

        [Test]
        public void IsTooltipShowing()
        {
            var game = new GameTestConstructor()
                .Build();

            var construction1 = game.CurrentLevel.FindViews<HandConstructionView>().First();
            Assert.IsNotNull(construction1);

            Assert.IsNull(game.CurrentLevel.FindView<HandConstructionTooltipView>());

            game.Engine.Controls.PointerEnter(construction1);

            Assert.IsNotNull(game.CurrentLevel.FindView<HandConstructionTooltipView>());

            game.Engine.Controls.PointerExit(construction1);

            Assert.IsNull(game.CurrentLevel.FindView<HandConstructionTooltipView>());

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
