using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
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

            var container = new ContainerViewPresenter(game.CurrentLevel);
            var prototype = new PrototypeViewPresenter(game.CurrentLevel, new HandConstructionViewPrefab());
            var hand = new HandViewPresenter(game.CurrentLevel, container, prototype);

            Assert.AreEqual(1, hand.Cards.Get<HandConstructionViewPresenter>().Count());

            game.Dispose();
        }

        private class HandConstructionViewPrefab : ViewPrefab<HandConstructionViewPresenter>
        {
            public override HandConstructionViewPresenter Create(ContainerViewPresenter conteiner)
            {
                return conteiner.Create((level) =>
                {
                    return new HandConstructionViewPresenter(level);
                });
            }
        }


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
