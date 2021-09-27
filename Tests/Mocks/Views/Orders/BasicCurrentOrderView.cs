using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicCurrentOrderView : TestView, ICurrentOrderView
    {
        public IRecipeView CreateRecipe()
        {
            return new TestRecipeView();
        }
    }
}
