using Game.Assets.Scripts.Game.Logic.Views.Game;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Game
{
    public class GameView : TestView, IGameView
    {
        public IGameSessionView Session { get; set; }

        public IGameSessionView CreateSession()
        {
            Session = new GameSessionView();
            return Session;
        }
        protected override void DisposeInner()
        {
            Session.Dispose();
        }
    }
}
