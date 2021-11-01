using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Clashes
{
    public class GameClashes
    { 
        public event Action OnClashStarted = delegate { };
        public event Action OnClashEnded = delegate { };

        public bool IsClashStarted { get; private set; }

        public void StartClash()
        {
            if (IsClashStarted) throw new Exception("Clash already started");

            IsClashStarted = true;
            OnClashStarted();
        }
    }
}
