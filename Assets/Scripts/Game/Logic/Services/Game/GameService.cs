using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;

namespace Game.Assets.Scripts.Game.Logic.Services.Game
{
    public class GameService : Disposable, IService
    {
        private IEngine _game;

        public GameService(IEngine game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        protected override void DisposeInner()
        {
        }

        public void Exit()
        {
            _game.Exit();
        }
    }

}
