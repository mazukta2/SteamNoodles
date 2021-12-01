using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Clashes
{
    public class GameClashes : Disposable
    { 
        public event Action OnClashStarted = delegate { };
        public event Action OnClashEnded = delegate { };

        public bool IsClashStarted { get; private set; }

        protected override void DisposeInner()
        {
        }

        public void StartClash()
        {
            if (IsClashStarted) throw new Exception("Clash already started");

            IsClashStarted = true;
            OnClashStarted();
        }

    }
}
