using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Clashes
{
    public class GameClashes : Disposable
    {

        public event Action OnClashStarted = delegate { };
        public event Action OnClashEnded = delegate { };

        public bool IsInClash { get; private set; }

        private IClashesSettings _settings;
        private GameTime _time;
        private GameTimer _clashTimer;

        public GameClashes(IClashesSettings settings, GameTime time)
        {
            _settings = settings ?? throw new  ArgumentNullException(nameof(settings));
            _time = time;
        }

        protected override void DisposeInner()
        {
            if (_clashTimer != null)
                _clashTimer.OnFinished -= StopClash;
        }

        public float GetClashesTime()
        {
            return _settings.ClashTime;
        }

        public void StartClash()
        {
            if (IsInClash) throw new Exception("Clash already started");

            IsInClash = true;
            _clashTimer = new GameTimer(_time, GetClashesTime());
            _clashTimer.OnFinished += StopClash;
            OnClashStarted();
        }

        private void StopClash()
        {
            if (!IsInClash) throw new Exception("Clash already stoped");

            _clashTimer.OnFinished -= StopClash;
            _clashTimer = null;
            IsInClash = false;
            OnClashEnded();
        }
    }
}
