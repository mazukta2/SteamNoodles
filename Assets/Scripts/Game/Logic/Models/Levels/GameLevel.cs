﻿using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
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
        public ConstructionsManager Constructions { get; private set; }

        private LevelDefinition _settings;

        public GameLevel(LevelDefinition settings, SessionRandom random, GameTime time, IDefinitions definitions)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            if (random == null) throw new ArgumentNullException(nameof(random));
            if (time == null) throw new ArgumentNullException(nameof(time));

            Hand = new PlayerHand(settings, settings.StartingHand);
            Constructions = new ConstructionsManager(definitions.Get<ConstructionsSettingsDefinition>(), _settings);
        }

        protected override void DisposeInner()
        {
            Hand.Dispose();
            Constructions.Dispose();
        }
    }
}
