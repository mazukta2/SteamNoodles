using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Presenters.Commands
{
    public class CommandsMock : ICommands
    {
        public List<ICommand> Commands { get; } = new List<ICommand>();

        public void Execute<T>(T command) where T : ICommand
        {
            Commands.Add(command);
        }

        public bool Only<T>()
        {
            if (Commands.Count != 1)
                return false;

            return Commands.First() is T;
        }

        public bool Last<T>()
        {
            if (Commands.Count == 0)
                return false;

            return Commands.Last() is T;
        }


        public bool IsEmpty()
        {
            return Commands.Count == 0;
        }
    }
}
