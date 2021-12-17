﻿using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Rewards;
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
        public IClashesSettings Settings => _settings;

        private IClashesSettings _settings;
        private GameTime _time;
        private readonly RewardCalculator _rewardCalculator;
        private readonly Placement _placement;
        private GameTimer _clashTimer;

        public GameClashes(IClashesSettings settings, Placement placement, GameTime time, RewardCalculator rewardCalculator)
        {
            _settings = settings ?? throw new  ArgumentNullException(nameof(settings));
            _time = time;
            _rewardCalculator = rewardCalculator ?? throw new ArgumentNullException(nameof(rewardCalculator));
            _placement = placement;
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
            if (_placement.Constructions.Count == 0) throw new Exception("Not constructions to start a clash");

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

            _rewardCalculator.Give(_settings.ClashReward);

            OnClashEnded();
        }
    }
}
