using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Views.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class LevelLoading : Disposable
    {
        public event Action<GameLevel> OnLoaded = delegate { };

        private GameSession _session;
        private ILevelDefinition _prototype;
        private ILevelsManager _levelManager;

        public LevelLoading(GameSession session, ILevelsManager levelManager, ILevelDefinition levelDefinition)
        {
            _session = session;
            _prototype = levelDefinition;
            _levelManager = levelManager;
            levelManager.Load(levelDefinition, Finished);
        }

        protected override void DisposeInner()
        {
        }

        private void Finished(ILevel lvl)
        {
            if (IsDisposed)
                return;

            var level = new GameLevel(_prototype, lvl, _session.GameRandom, _session.Time);
            HandleLevelModelCreated(level, lvl);
            OnLoaded(level);
            Dispose();
        }

        private void HandleLevelModelCreated(GameLevel level, ILevel lvl)
        {
            lvl.FindObject<CurrentLevelSource>()?.SetLevel(level);
        }
    }
}
