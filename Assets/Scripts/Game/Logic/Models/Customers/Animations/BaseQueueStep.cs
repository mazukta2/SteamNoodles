using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.Animations
{
    public abstract class BaseQueueStep : Disposable
    {
        public event Action OnFinished = delegate { }; 
        public abstract void Play();

        protected void FireOnFinished()
        {
            OnFinished();
        }
    }
}
