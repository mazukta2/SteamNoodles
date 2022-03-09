using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Models.Session
{
    public class GameSession : Disposable
    {
        public SessionRandom GameRandom { get; private set; } = new SessionRandom();
        public GameTime Time { get; private set; } = new GameTime();

        private ILevelsManager _levelsManager;

        public GameSession(ILevelsManager levelsManager)
        {
            _levelsManager = levelsManager;
        }

        protected override void DisposeInner()
        {
            Time.Dispose();
        }

        public LevelLoading LoadLevel(LevelDefinition definition)
        {
            return new LevelLoading(this, _levelsManager, definition);
        }

    }
}