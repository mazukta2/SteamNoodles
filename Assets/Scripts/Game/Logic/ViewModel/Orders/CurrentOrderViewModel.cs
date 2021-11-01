﻿using Game.Assets.Scripts.Game.Logic.Models.Orders;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.ViewModel.Orders
{
    public class CurrentOrderViewModel
    {
        //public List<RecipeViewModel> Recipies { get; } = new List<RecipeViewModel>();
        public ICurrentOrderView View { get; private set; }

        private ServingOrderProcess _model;

        public CurrentOrderViewModel(ServingOrderProcess model, ICurrentOrderView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));

            _model = model;
            View = view;

            //foreach (var recipe in _model.Recipes)
            //{
            //    Recipies.Add(new RecipeViewModel(recipe, View.CreateRecipe()));
            //}
        }

        public void Destroy()
        {
            //foreach (var item in Recipies)
            //    item.Destroy();

            View.Destroy();
            View = null;
        }
    }
}
