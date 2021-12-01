using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Levels;
using Game.Tests.Mocks.Prototypes.Levels;
using Game.Tests.Mocks.Views.Game;
using Game.Tests.Mocks.Views.Levels;
using Tests.Assets.Scripts.Game.Logic.Presenters;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Tests.Shortcuts
{
    public class LevelShortcuts
    {
        public (GameLevel, LevelPresenter, ILevelView) LoadLevel()
        {
            return LoadLevel(new TestLevelPrototype());
        }

        public (GameLevel, LevelPresenter, ILevelView) LoadLevel(TestLevelPrototype proto)
        {
            var game = new GameLogic();
            var view = new GameView();
            var vm = new GameLogicPresenter(game, view);

            game.CreateSession();
            vm.Session.LoadLevel(proto);
            proto.Finish();

            return (game.Session.CurrentLevel, vm.Session.CurrentLevel, view.Session.CurrentLevel);
        }
    }
}