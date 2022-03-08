using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class LevelLoading : Disposable
    {
        public event Action<GameLevel> OnLoaded = delegate { };

        private GameSession _session;
        private ILevelDefinition _prototype;
        private ILevelsManager _levelManager;
        private GameLevel _level;

        private bool _isLoaded = false;

        public LevelLoading(GameSession session, ILevelsManager levelManager, ILevelDefinition levelDefinition)
        {
            _session = session;
            _prototype = levelDefinition;
            _levelManager = levelManager;
            _level = new GameLevel(_prototype, _session.GameRandom, _session.Time);

            levelManager.Load(_level, levelDefinition, Finished);
        }

        protected override void DisposeInner()
        {
            if (!_isLoaded)
                _level.Dispose();
        }

        private void Finished(ILevel lvl)
        {
            if (IsDisposed)
                return;

            _isLoaded = true;

            OnLoaded(_level);
            Dispose();
        }
    }
}
