using Game.Assets.Scripts.Game.Logic.Common.Core;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Session
{
    public class GameSession : Disposable, IGameSession
    {
        private readonly GameModel _gameModel;

        public GameSession(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        protected override void DisposeInner()
        {
        }

        public void StartLastAvailableLevel()
        {
            _gameModel.StartNewGame();
        }
    }
}