using Assets.Scripts.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers
{
    public class WorkManager
    {
        private GameLevel _level;

        public WorkManager(GameLevel level)
        {
            _level = level;

            HandleOrder();
        }

        public Job MainHeroJob { get; private set; }

        public void HandleOrder()
        {
            var currentOrder = _level.Orders?.CurrentOrder;
            if (currentOrder != null && MainHeroJob == null)
            {
                var recipe = _level.Orders.CurrentOrder.Recipes.Where(x => x.IsOpen()).FirstOrDefault();
                if (recipe != null)
                {
                    MainHeroJob = new RecipeJob(this, recipe);
                    MainHeroJob.Start();
                }
                else
                {
                    // all recipies is compited. do nothing
                }
            }
        }

        public void FinishCurrentJob()
        {
            if (MainHeroJob != null)
            {
                MainHeroJob.Stop();
                MainHeroJob = null;
            }
            HandleOrder();
        }
    }
}
