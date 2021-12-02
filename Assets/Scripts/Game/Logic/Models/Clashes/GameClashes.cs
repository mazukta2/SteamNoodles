using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Clashes
{
    public class GameClashes : Disposable
    {
        private IClashesSettings _settings;

        public event Action OnClashStarted = delegate { };
        public event Action OnClashEnded = delegate { };

        public bool IsInClash { get; private set; }

        public GameClashes(IClashesSettings settings)
        {
            _settings = settings ?? throw new  ArgumentNullException(nameof(settings));
        }

        protected override void DisposeInner()
        {
        }

        public float GetClashesTime()
        {
            return _settings.ClashTime;
        }

        public void StartClash()
        {
            if (IsInClash) throw new Exception("Clash already started");

            IsInClash = true;
            OnClashStarted();
        }
    }
}
