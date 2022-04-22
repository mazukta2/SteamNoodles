using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views
{
    public abstract class View : Disposable, IView
    {
        public LevelView Level { get; private set; }

        public View(LevelView level)
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
