using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Animations
{
    public abstract class BaseSequenceStep : Disposable
    {
        public event Action OnFinished = delegate { };
        public abstract void Play();

        protected void FireOnFinished()
        {
            OnFinished();
        }
    }
}
