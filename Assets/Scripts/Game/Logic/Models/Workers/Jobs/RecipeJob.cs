using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.States.Game.Level;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs
{
    public class RecipeJob : Job
    {
        private StateLink<JobState> _state;

        public RecipeJob(StateLink<JobState> state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            _state = state;

            //if (!recipe.IsOpen()) throw new Exception("Recipe is closed");
            //if (recipe.GetConstruction() == null) throw new Exception("Not construction to resolve the recipe");

            //_recipe = recipe;
            //_work = work;
            //_construction = _recipe.GetConstruction();
        }

        public override void Start()
        {
            //_updater = _time.MakeUpdater(_construction.WorkTime);
            //_updater.OnUpdate += HitRecipe;
        }

        public override void Stop()
        {
            //if (_updater != null)
            //{
            //    _updater.OnUpdate -= HitRecipe;
            //    _updater.Destroy();
            //}
        }

        private void HitRecipe()
        {
            //_recipe.Progress(_construction.WorkProgressPerHit);
        }
    }
}
