using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Levels;
using Game.Tests.Mocks.Prototypes.Levels;
using Tests.Assets.Scripts.Game.Logic.Presenters;

namespace Game.Tests.Shortcuts
{
    public class LevelShortcuts
    {
        public (GameLevel, LevelPresenter, TestLevelPrototype) LoadLevel()
        {
            return LoadLevel(new TestLevelPrototype());
        }

        public (GameLevel, LevelPresenter, TestLevelPrototype) LoadLevel(TestLevelPrototype proto)
        {
            var game = new GameLogic();
            var vm = new GameLogicPresenter(game);

            game.CreateSession();
            vm.Session.LoadLevel(proto);
            proto.Finish();

            return (game.Session.CurrentLevel, vm.Session.CurrentLevel, proto);
        }
    }
}