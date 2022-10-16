using System;
using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure
{
    // Infrastructure is a basic game layer. Without infrastructure game don't work at all.
    public interface IInfrastructure
    {
        public static IInfrastructure Default { get; set; } = new DefaultInfrastructure();

        public Core Core { get; }
        void ConnectEnviroment(UnityEnviroment unityEnviroment, bool autoStart = true);
    }
}

