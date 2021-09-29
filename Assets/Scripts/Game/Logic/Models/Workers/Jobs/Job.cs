using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs
{
    public abstract class Job
    {
        public abstract void Start();
        public abstract void Stop();
    }
}
