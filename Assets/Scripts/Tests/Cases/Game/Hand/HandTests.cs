using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using Game.Assets.Scripts.Tests.Managers.Game;
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


        private class HandConstructionViewPrefab : MockViewPrefab
        {
            public override IView CreateView<T>(ILevel level, MockContainerView container)
            {
                var buttonView = new ButtonView(level);
                var imageView = new ImageView(level);
                return new HandConstructionView(level, buttonView, imageView);
            }
        }

        [Test]
        public void IsIconSettedInHand()
        {
            var game = new GameTestConstructor()
                .Build();

            var construction = game.Core.Engine.Definitions.Get<ConstructionDefinition>("Construction1");

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

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
