using System;
using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure
{
    public interface IInfrastructure
    {
        public static IInfrastructure Default { get; set; }

        public Core Core { get; }
    }
}

