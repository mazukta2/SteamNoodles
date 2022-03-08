using NUnit.Framework;

namespace Game.Tests.Cases.Constructions
{
    public class HandTests
    {
        /*
        [Test]
        public void IsHandLimitWorking()
        {
            var game = new GameController();
            var (models, _, views) = game.LoadLevel();

            var construction1 = models.Hand.Cards.First().Settings;
            var consteuction2 = new ConstructionSettings();
            var settings = (LevelSettings)models.Clashes.Settings;
            settings.HandSize = 3;

            Assert.AreEqual(1, models.Hand.Cards.Count());

            for (int i = 0; i < 5; i++)
            {
                models.Hand.Add(consteuction2);
            }

            Assert.AreEqual(3, models.Hand.Cards.Count());
            foreach (var mod in models.Hand.Cards)
                Assert.AreEqual(consteuction2, mod.Settings);

            game.Exit();
        }
        */

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
