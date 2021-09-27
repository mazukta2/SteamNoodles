using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Models.Orders;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class CurrentOrderViewModel
    {
        public List<RecipeViewModel> Recipies { get; } = new List<RecipeViewModel>();

        private CurrentOrder _model;

        public CurrentOrderViewModel(CurrentOrder model, ICurrentOrderView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            View = view;

            foreach (var recipe in _model.Recipes)
            {
                Recipies.Add(new RecipeViewModel(recipe, View.CreateRecipe()));
            }
        }

        public ICurrentOrderView View { get; private set; }

        public void Destroy()
        {
            foreach (var item in Recipies)
                item.Destroy();

            View.Destroy();
            View = null;
        }
    }
}
