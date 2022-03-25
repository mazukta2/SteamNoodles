using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Environment.Definitions.List;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
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
            Assert.AreEqual(1, hand.Cards.Get<HandConstructionView>().Count());

            game.Dispose();
        }

        [Test]
        public void IsYouGetNewCardsAfterBuilding()
        {
            var game = new GameTestConstructor()
                .Build();

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();
            game.Engine.Controls.Click();

            Assert.AreEqual(1, game.CurrentLevel.FindViews<HandConstructionView>().Count());

            game.Dispose();
        }


        private class HandConstructionViewPrefab : ViewPrefab
        {
            public override View Create<T>(ContainerView conteiner)
            {
                return conteiner.Create((level) =>
                {
                    var buttonView = new ButtonView(level);
                    var imageView = new ImageView(level);
                    return new HandConstructionView(level, buttonView, imageView);
                });
            }
        }

        //[Test]
        //public void IsIconSettedInHand()
        //{
        //    var game = new GameController();
        //    var (models, presenters, views) = game.LoadLevel();
        //    var item = views.Screen.Hand.Cards.List.First();
        //    var icon = item.GetIcon();
        //    Assert.IsTrue(models.Hand.Cards.First().HandIcon.Equals(icon));
        //    game.Exit();
        //}

        //[Test]
        //public void IsHandLimitWorking()
        //{
        //    var game = new GameTestConstructor(new DefaultSettings()).Build();

        //    //var construction1 = models.Hand.Cards.First().Settings;
        //    //var consteuction2 = new ConstructionSettings();
        //    //var settings = (LevelSettings)models.Clashes.Settings;
        //    //settings.HandSize = 3;

        //    //Assert.AreEqual(1, game.GameModel.CreateSession.Hand.Cards.Count());

        //    //for (int i = 0; i < 5; i++)
        //    //{
        //    //    models.Hand.Add(consteuction2);
        //    //}

        //    //Assert.AreEqual(3, models.Hand.Cards.Count());
        //    //foreach (var mod in models.Hand.Cards)
        //    //    Assert.AreEqual(consteuction2, mod.Settings);

        //    game.Dispose();
        //}

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
