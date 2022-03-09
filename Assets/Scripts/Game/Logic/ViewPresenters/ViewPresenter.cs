using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.ViewPresenters
{
    public abstract class ViewPresenter : Disposable
    {
        protected ILevel Level { get; private set; }
        
        public ViewPresenter(ILevel level)
        {
            Level = level;
            Level.OnDispose += Dispose;
            Level.Add(this);
        }

        protected override void Dispose(bool disposing)
        {
            Level.OnDispose -= Dispose;
            Level.Remove(this);
            base.Dispose(disposing);
        }
    }
}
