using Assets.Scripts.Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.ViewModel;
using Tests.Mocks.Prototypes.Levels;

namespace Tests.Tests.Cases.GameFlow
{
    public class LevelTests
    {
        [Test]
        public void Loading()
        {
            var game = new GameLogic();
            var gameVm = new GameLogicViewModel(game);
            game.CreateSession();
            var session = game.Session;
            var sessionVm = gameVm.Session.Value;
            Assert.IsTrue(sessionVm != null);

            var levelProto = new BasicLevelPrototype();
            gameVm.Session.Value.LoadLevel(levelProto);
            Assert.IsTrue(session.CurrentLevel == null);
            Assert.IsTrue(sessionVm.CurrentLevel.Value == null);
            Assert.IsTrue(session.IsNotLoaded);

            levelProto.Finish();

            Assert.IsTrue(session.CurrentLevel != null);
            Assert.IsTrue(sessionVm.CurrentLevel.Value != null);
            Assert.IsTrue(!session.IsNotLoaded);

        }
    }
}
