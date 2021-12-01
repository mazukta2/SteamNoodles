using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Tests.Mocks.Views.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Game
{
    public class GameSessionView : TestView, IGameSessionView
    {
        public ILevelView CurrentLevel { get; set; }

        public void SetLevel(ILevelView levelView)
        {
            CurrentLevel = levelView;
        }
    }
}
