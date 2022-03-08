using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class GameLevel : Disposable
    {
        public event Action OnMoneyChanged = delegate { };

        private ILevelDefinition _settings;
        private ILevel _level;

        public GameLevel(ILevelDefinition settings, ILevel level, SessionRandom random, GameTime time)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _level = level ?? throw new ArgumentNullException(nameof(level));
            if (random == null) throw new ArgumentNullException(nameof(random));
            if (time == null) throw new ArgumentNullException(nameof(time));
        }

        protected override void DisposeInner()
        {
            _level.Dispose();
        }
    }
}
