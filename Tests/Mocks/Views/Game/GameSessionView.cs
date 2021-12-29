using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Tests.Mocks.Views.Levels;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Game
{
    public class GameSessionView : TestView, IGameSessionView
    {
        private readonly LevelView _level = new LevelView();

        protected override void DisposeInner()
        {
            _level.Dispose();
        }

        public ILevelView GetCurrentLevel()
        {
            return _level;
        }
    }
}
