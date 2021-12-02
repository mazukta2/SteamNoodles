using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Game
{
    public class GameView : TestView, IGameView
    {
        public DisposableViewKeeper<IGameSessionView> Session { get; } = new DisposableViewKeeper<IGameSessionView>(CreateSession);

        private static IGameSessionView CreateSession()
        {
            return new GameSessionView();
        }

        protected override void DisposeInner()
        {
            Session.Dispose();
        }
    }
}
