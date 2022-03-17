using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class LevelLoading : Disposable
    {
        public event Action<GameLevel> OnLoaded = delegate { };

        private GameSession _session;
        private LevelDefinition _prototype;
        private ILevelsManager _levelManager;
        private GameLevel _level;

        private bool _isLoaded = false;

        public LevelLoading(GameSession session, IGameEngine gameEngine, LevelDefinition levelDefinition)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _prototype = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
            _levelManager = gameEngine.Levels ?? throw new ArgumentNullException(nameof(gameEngine));
            if (!gameEngine.Definitions.GetList<LevelDefinition>().Any(x => x == levelDefinition))
                throw new Exception($"Can't find level {levelDefinition} in settings");

            _level = new GameLevel(_prototype, _session.GameRandom, _session.Time, gameEngine.Definitions);

            _levelManager.Load(_level, levelDefinition, Finished);
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
