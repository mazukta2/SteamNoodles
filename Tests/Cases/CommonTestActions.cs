using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Tests.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Tests.Cases
{
    public static class CommonTestActions
    {
        public static void ServeCustumer(GameController game, GameLevel models)
        {
            var customer = models.Customers.ServingCustomer.Unit;
            customer.TeleportToTarget();
            game.PushTime(3);
            customer.TeleportToTarget();
        }

        public static void BuildConstruction(GameController game, ILevelView views)
        {
            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));
            views.Screen.Clashes.Value.StartClash.Click();
        }

    }
}
