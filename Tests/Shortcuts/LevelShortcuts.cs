using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.ViewModel.Levels;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.ViewModel;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Session;
using Tests.Mocks.Prototypes.Levels;

namespace Tests.Tests.Shortcuts
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