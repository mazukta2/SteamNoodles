using System;
using Game.Assets.Scripts.Game.Environment;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure
{
    public class DefaultInfrastructure : IInfrastructure
    {
        public Core Core { get; }

        public DefaultInfrastructure(Core core)
        {
            Core = core;
        }
    }
}

