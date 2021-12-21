using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Rewards;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Clashes
{
    public class GameClashes : Disposable
    {
        public event Action OnClashStarted = delegate { };
        public event Action OnClashEnded = delegate { };

        public bool IsInClash => _currentClash != null;
        public Clash CurrentClash => _currentClash;
        public IClashesSettings Settings => _settings;

        private IClashesSettings _settings;
        private GameTime _time;
        private readonly RewardCalculator _rewardCalculator;
        private readonly Placement _placement;
        private readonly GameLevel _level;
        private readonly IUnitsSettings _unitsSettings;
        private readonly LevelUnits _units;
        private readonly SessionRandom _random;
        private Clash _currentClash;

        public GameClashes(IClashesSettings settings,Placement placement, GameTime time, RewardCalculator rewardCalculator,
            GameLevel level, IUnitsSettings unitsSettings, LevelUnits units, SessionRandom random)
        {
            _settings = settings ?? throw new  ArgumentNullException(nameof(settings));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _rewardCalculator = rewardCalculator ?? throw new ArgumentNullException(nameof(rewardCalculator));
            _placement = placement ?? throw new ArgumentNullException(nameof(placement));
            _level = level ?? throw new ArgumentNullException(nameof(level));
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _units = units ?? throw new ArgumentNullException(nameof(units));
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        protected override void DisposeInner()
        {
            if (_currentClash != null)
            {
                _currentClash.OnClashStoped -= _currentClash_OnClashStoped;
                _currentClash.Dispose();
            }
        }

        public void StartClash()
        {
            if (IsInClash) throw new Exception("Clash already started");
            if (_placement.Constructions.Count == 0) throw new Exception("Not constructions to start a clash");

            _currentClash = new Clash(this, _level, _settings, _unitsSettings, _placement, _rewardCalculator, _units, _time, _random);
            _currentClash.OnClashStoped += _currentClash_OnClashStoped;
            OnClashStarted();
        }

        private void _currentClash_OnClashStoped()
        {
            _currentClash = null;
            OnClashEnded();
        }
    }
}
