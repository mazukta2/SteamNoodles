using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Tests.Cases;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class CommandsTests
    {
        [Test, Order(TestCore.EssentialOrder)]
        public void IsCommandsHandles()
        {
            var commands = new CommandManager();
            commands.Add(new CommandHandler());

            var c1 = new Command();
            var c2 = new Command2();

            commands.Execute(c1);
            commands.Execute(c2);

            Assert.IsTrue(c1.IsDone);
            Assert.IsFalse(c2.IsDone);
        }

        private class CommandHandler : ICommandHandler<Command>
        {
            public void Handle(Command command)
            {
                command.Execute();
            }
        }

        private class Command : ICommand
        {
            public bool IsDone;
            public void Execute()
            {
                IsDone = true;
            }
        }

        private class Command2 : ICommand
        {
            public bool IsDone;
            public void Execute()
            {
                IsDone = true;
            }
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
