using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Logic;
using Tests.Mocks.Prototypes.Levels;

namespace Tests.Buildings
{
    public class BuildingsTests
    {
        [Test]
        public void Test()
        {
            var game = new GameLogic();
            var session = game.CreateSession();
            var level = session.LoadLevel(new BasicLevelPrototype());
        }
    }
}
