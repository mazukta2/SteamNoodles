using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Orders;

namespace Tests.Assets.Scripts.Game.Logic.Views
{
    public interface ICurrentOrderView : IView
    {
        IRecipeView CreateRecipe();
    }
}
