using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Sources;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Environment.Definitions.Constructions;
using Game.Assets.Scripts.Tests.Environment.Definitions.List;
using Game.Assets.Scripts.Tests.Environment.Game.ConstructorSettings;
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
            var construction = new ConstructionDefinitionInTests();
            var game = new GameTestConstructor()
                .LoadDefinitions(new DefaultDefinitions())
                .AddAndLoadLevel(new LevelDefinitionInTests(new BasicSellingLevel()) {
                        StartingHand = new List<IConstructionDefinition>() { construction }
                })
                .Build();

            var source = game.CurrentLevel.Add(new CurrentLevelSource());
            var container = game.CurrentLevel.Add(new ViewContainer());
            var hand = game.CurrentLevel.Add(new HandView()
            {
                Level = source,
                Cards = container
            });
            Assert.AreEqual(1, hand.Cards.Get<HandConstructionView>().Count());

            game.Dispose();
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
