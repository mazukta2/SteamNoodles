using Game.Assets.Scripts.Game.Logic.States;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers
{
    public class WorkManager
    {
        private State _state;

        public WorkManager(State state)
        {
            _state = state;

            HandleOrder();
        }

        //public Job MainHeroJob { get; private set; }

        public void HandleOrder()
        {
            //var currentOrder = _level.Orders?.CurrentOrder;
            //if (currentOrder != null && MainHeroJob == null)
            //{
            //    var recipe = _level.Orders.CurrentOrder.Recipes.Where(x => x.IsOpen()).FirstOrDefault();
            //    if (recipe != null)
            //    {
            //        //MainHeroJob = new RecipeJob(this, recipe);
            //        MainHeroJob.Start();
            //    }
            //    else
            //    {
            //        // all recipies is compited. do nothing
            //    }
            //}
        }

        public void FinishCurrentJob()
        {
            //if (MainHeroJob != null)
            //{
            //    MainHeroJob.Stop();
            //    MainHeroJob = null;
            //}
            //HandleOrder();
        }
    }
}
