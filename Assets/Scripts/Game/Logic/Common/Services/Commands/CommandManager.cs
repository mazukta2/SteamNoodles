using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Commands
{
    public class CommandManager : ICommands
    {
        private List<IBaseCommandHandler> _list = new List<IBaseCommandHandler>();

        public void Add(IBaseCommandHandler handler)
        {
            _list.Add(handler);
        }

        public void Remove(IBaseCommandHandler handler)
        {
            _list.Remove(handler);
        }

        public void Execute<T>(T command) where T : ICommand
        {
            foreach (var handler in _list)
            {
                if (handler is ICommandHandler<T> commandHandler)
                    commandHandler.Handle(command);
            }
        }

    }
}
