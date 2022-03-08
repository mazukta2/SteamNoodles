using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Core
{
    public abstract class Disposable : PrivateDisposable, IDisposable
    {
        public new void Dispose()
        {
            base.Dispose();
        }
    }
}
