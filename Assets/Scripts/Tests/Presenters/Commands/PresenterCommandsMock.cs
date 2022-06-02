using Game.Assets.Scripts.Game.Logic.Presenters.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Presenters.Commands
{
    public class PresenterCommandsMock : IPresenterCommands
    {
        public List<IPresenterCommand> Commands { get; } = new List<IPresenterCommand>();

        public void Execute(IPresenterCommand command)
        {
            Commands.Add(command);
        }

        public void ExecuteAll()
        {
            foreach (var command in Commands)
                command.Execute();

            Commands.Clear();
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
