using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Tests.Controllers;
using System.Linq;

namespace Game.Tests.Cases
{
    public static class CommonTestActions
    {
        public static void ServeCustumer(GameController game, GameLevel models)
        {
            var customer = models.Clashes.CurrentClash.Customers.GetCustomers().First().Unit;
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
