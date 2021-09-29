using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs
{
    public class RecipeJob : Job
    {
        private Recipe _recipe;
        private WorkManager _work;
        private TimeManager _time;
        private Construction _construction;
        private GameUpdater _updater;

        public RecipeJob(WorkManager work, TimeManager time, Recipe recipe)
        {
            if (recipe == null) throw new ArgumentNullException(nameof(recipe));
            if (!recipe.IsOpen()) throw new Exception("Recipe is closed");
            if (recipe.GetConstruction() == null) throw new Exception("Not construction to resolve the recipe");

            _recipe = recipe;
            _work = work;
            _time = time;
            _construction = _recipe.GetConstruction();
        }

        public override void Start()
        {
            _updater = _time.MakeUpdater(_construction.WorkTime);
            _updater.OnUpdate += HitRecipe;
        }

        public override void Stop()
        {
            if (_updater != null)
            {
                _updater.OnUpdate -= HitRecipe;
                _updater.Destroy();
            }
        }

        private void HitRecipe()
        {
            _recipe.Progress(_construction.WorkProgressPerHit);
        }
    }
}
