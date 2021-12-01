using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Views.Orders;
using System;

namespace Tests.Assets.Scripts.Game.Logic.Presenters.Levels
{
    public class RecipePresenter
    {
        private Recipe _model;

        public RecipePresenter(Recipe model, IRecipeView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            View = view;

            View.SetName(_model.Ingredient.Name);
            View.SetMaxCount((int)_model.MaxProgress);
            View.SetCount((int)_model.CurrentProgress);

            _model.OnProcess += _model_OnProcess;
        }

        public void Destroy()
        {
            _model.OnProcess -= _model_OnProcess;
            View.Destroy();
            View = null;
        }

        public IRecipeView View { get; private set; }

        private void _model_OnProcess()
        {
            View.SetCount((int)_model.CurrentProgress);
        }

    }
}
