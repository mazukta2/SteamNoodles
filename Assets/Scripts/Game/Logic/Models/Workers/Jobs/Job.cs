using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs
{
    public abstract class Job
    {
        public abstract void Stop();

        public static Job MakeJob(State state, uint id)
        {
            var job = state.Get(id);
            if (job is RecipeJob.GameState)
                return new RecipeJob(state, id);

            throw new Exception("Unknown job type");
        }
    }
}
