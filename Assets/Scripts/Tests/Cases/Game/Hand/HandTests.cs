using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Environment.Definitions.List;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Tests.Cases.Constructions
{
    public class HandTests
    {
        [Test]
        public void IsFirstCardSpawned()
        {
            var construction = new ConstructionDefinition();
            var game = new GameTestConstructor()
                .LoadDefinitions(new DefaultDefinitions())
                .AddAndLoadLevel(new LevelDefinitionMock(new BasicSellingLevel())
                {
                    StartingHand = new List<ConstructionDefinition>() { construction }
                })
                .Build();

            var hand = game.CurrentLevel.FindView<HandView>();
            Assert.AreEqual(1, hand.Cards.Get<HandConstructionView>().Count());

            game.Dispose();
        }

        private class HandConstructionViewPrefab : ViewPrefab
        {
            public override object Create<T>(ContainerView conteiner)
            {
                return conteiner.Create((level) =>
                {
                    var buttonView = new ButtonView(level);
                    return new HandConstructionView(level, buttonView);
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
