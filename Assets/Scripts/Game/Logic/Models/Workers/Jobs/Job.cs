using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs
{
    public abstract class Job
    {
        public event Action OnStop = delegate { };

        public bool IsStopped { get; private set; }
        protected abstract void StopInner();
        public abstract void OnTime();

        public void Stop()
        {
            IsStopped = true;
            StopInner();
            OnStop();
        }
    }
}
