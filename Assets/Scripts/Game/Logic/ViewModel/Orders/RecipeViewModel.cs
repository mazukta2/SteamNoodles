using Game.Assets.Scripts.Game.Logic.Models.Orders;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class RecipeViewModel
    {
        private Recipe _model;

        public RecipeViewModel(Recipe model, IRecipeView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            View = view;
        }

        public IRecipeView View { get; private set; }

        public void Destroy()
        {
            View.Destroy();
            View = null;
        }
    }
}
