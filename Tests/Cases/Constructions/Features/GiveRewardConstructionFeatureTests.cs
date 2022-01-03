using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Buildings;
using Game.Tests.Mocks.Settings.Buildings.Features;
using Game.Tests.Mocks.Settings.Customers.Features;
using Game.Tests.Mocks.Settings.Levels;
using Game.Tests.Mocks.Settings.Rewards;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Customers
{
    public class GiveRewardConstructionFeatureTests
    {
        [Test]
        public void IsRewardCardsWorking()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            Assert.AreEqual(1, models.Units.GetPool().Count());

            var rewardConstr = new ConstructionSettings();
            var buildingConstr = new ConstructionSettings()
            {
                FeaturesList = new List<IConstructionFeatureSettings> {
                    new GiveRewardOnBuildConstructionFeatureSettings(new Reward()
                    {
                        ToHandSource = new Dictionary<IConstructionSettings, int>()
                        {
                            {rewardConstr, 1},
                        },
                        MinToHand = 1,
                        MaxToHand = 1,
                    })
                }
            };

            models.Hand.Add(buildingConstr);
            Assert.AreEqual(2, models.Hand.Cards.Count());
            Assert.AreEqual(buildingConstr, models.Hand.Cards.Last().Settings);

            views.Screen.Hand.Cards.List.Last().Button.Click();
            views.Placement.Click(System.Numerics.Vector2.Zero);

            Assert.AreEqual(2, models.Hand.Cards.Count());
            Assert.AreEqual(rewardConstr, models.Hand.Cards.Last().Settings);

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
