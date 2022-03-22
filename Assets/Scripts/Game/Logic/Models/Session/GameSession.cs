using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Models.Session
{
    public class GameSession : Disposable
    {
        public SessionRandom GameRandom { get; private set; } = new SessionRandom();

        private IGameEngine _engine;

        public GameSession(IGameEngine engine)
        {
            _engine = engine;
        }

        protected override void DisposeInner()
        {
        }

        public LevelLoading LoadLevel(LevelDefinition definition)
        {
            return new LevelLoading(this, _engine, definition);
        }

    }
}