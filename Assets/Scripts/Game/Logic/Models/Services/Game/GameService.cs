using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models
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
