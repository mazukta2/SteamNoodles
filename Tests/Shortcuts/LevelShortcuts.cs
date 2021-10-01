using Assets.Scripts.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.ViewModel.Levels;
using Game.Tests.Mocks.Prototypes.Levels;
using Tests.Assets.Scripts.Game.Logic.ViewModel;

namespace Game.Tests.Shortcuts
{
    public class LevelShortcuts
    {
        public (GameLevel, LevelViewModel, TestLevelPrototype) LoadLevel()
        {
            return LoadLevel(new TestLevelPrototype());
        }

        public (GameLevel, LevelViewModel, TestLevelPrototype) LoadLevel(TestLevelPrototype proto)
        {
            var game = new GameLogic();
            var vm = new GameLogicViewModel(game);

            game.CreateSession();
            vm.Session.LoadLevel(proto);
            proto.Finish();

            return (game.Session.CurrentLevel, vm.Session.CurrentLevel, proto);
        }
    }
}