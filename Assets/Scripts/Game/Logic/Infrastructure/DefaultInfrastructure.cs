using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure
{
    public class DefaultInfrastructure : IInfrastructure
    {
        private UnityEnviroment _enviroment;

        public Core Core { get; private set; }
        public DefaultInfrastructure()
        {
        }

        public void ConnectEnviroment(UnityEnviroment unityEnviroment, bool autoStart = true)
        {
            _enviroment = unityEnviroment;
            _enviroment.OnDispose += DisconnectEnviroment;

            Core = new Core(unityEnviroment);

            if (autoStart)
                Core.Start();
        }

        private void DisconnectEnviroment()
        {
            _enviroment.OnDispose -= DisconnectEnviroment;
            Core.Dispose();
        }
    }
}

