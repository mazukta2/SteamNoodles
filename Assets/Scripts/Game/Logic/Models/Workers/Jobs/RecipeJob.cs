using Game.Assets.Scripts.Game.Logic.Models.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs
{
    public class RecipeJob : Job
    {
        private Recipe _recipe;
        private WorkManager _work;

        public RecipeJob(WorkManager work, Recipe recipe)
        {
            if (recipe == null) throw new ArgumentNullException(nameof(recipe));
            if (!recipe.IsOpen()) throw new Exception("Recipe is closed");
            if (recipe.GetConstruction() == null) throw new Exception("Not construction to resolve the recipe");

            _recipe = recipe;
            _work = work;
        }

        public override void Start()
        {
            var construction = _recipe.GetConstruction();
            _work.
        }

        public override void Stop()
        {
        }
    }
}
