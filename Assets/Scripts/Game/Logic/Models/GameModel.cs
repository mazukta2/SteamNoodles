using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Session;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public class GameModel : Disposable
    {
        private readonly IGameEngine _controller;

        public GameModel(IGameEngine controller)
        {
            _controller = controller;
        }

        protected override void DisposeInner()
        {
        }

        public GameSession CreateSession()
        {
            return new GameSession(_controller.Levels);
        }
    }

}
