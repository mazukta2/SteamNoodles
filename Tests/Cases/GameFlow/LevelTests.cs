using Assets.Scripts.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Tests.Mocks.Prototypes.Levels;
using Game.Tests.Mocks.Views.Game;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Presenters;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.GameFlow
{
    public class LevelTests
    {
        [Test]
        public void Loading()
        {
            var game = new GameLogic();
            var view = new GameView();
            var gameVm = new GameLogicPresenter(game, view);
            game.CreateSession();
            var session = game.Session;
            var sessionVm = gameVm.Session;
            Assert.IsTrue(sessionVm != null);

            var levelProto = new TestLevelPrototype();
            gameVm.Session.LoadLevel(levelProto);
            Assert.IsTrue(session.CurrentLevel == null);
            Assert.IsTrue(sessionVm.CurrentLevel == null);
            Assert.IsTrue(session.IsNotLoaded);

            levelProto.Finish();

            Assert.IsTrue(session.CurrentLevel != null);
            Assert.IsTrue(sessionVm.CurrentLevel != null);
            Assert.IsTrue(!session.IsNotLoaded);

            gameVm.Dispose();
        }

        //[TearDown]
        //public void UnhandledExceptionRegistering()
        //{
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    //Assert.IsTrue(Disposable.IsEverythingDisposed());
        //}

    }
}
