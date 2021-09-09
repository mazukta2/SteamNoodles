using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Models.Levels;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.ViewModel;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;
using Tests.Mocks.Prototypes.Levels;

namespace Tests.Tests.Shortcuts
{
    public class LevelShortcuts
    {
        public (GameLevel, LevelViewModel) LoadLevel()
        {
            var game = new GameLogic();
            var vm = new GameLogicViewModel(game);

            game.CreateSession();
            var levelProto = new BasicLevelPrototype();
            game.Session.LoadLevel(levelProto);
            levelProto.Finish();

            vm.Update();
            return (game.Session.CurrentLevel, vm.Session.CurrentLevel);
        }
}
    }
