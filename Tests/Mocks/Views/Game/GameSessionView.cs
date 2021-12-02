using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Tests.Mocks.Views.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Game
{
    public class GameSessionView : TestView, IGameSessionView
    {
        public DisposableViewSetter<ILevelView> CurrentLevel { get; } = new DisposableViewSetter<ILevelView>();

        protected override void DisposeInner()
        {
            CurrentLevel.Dispose();
        }
    }
}
