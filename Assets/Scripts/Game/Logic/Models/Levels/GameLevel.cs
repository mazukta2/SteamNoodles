using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class GameLevel : Disposable
    {
        public event Action OnMoneyChanged = delegate { };

        public PlayerHand Hand { get; private set; }

        private LevelDefinition _settings;

        public GameLevel(LevelDefinition settings, SessionRandom random, GameTime time)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            if (random == null) throw new ArgumentNullException(nameof(random));
            if (time == null) throw new ArgumentNullException(nameof(time));

            Hand = new PlayerHand(settings, settings.StartingHand);
        }

        protected override void DisposeInner()
        {
            Hand.Dispose();
        }
    }
}
