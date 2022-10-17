using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure
{
    public class DefaultInfrastructure : IInfrastructure
    {
        private UnityEnviroment _enviroment;

        public GameApplication Application { get; private set; }
        public DefaultInfrastructure()
        {
        }

        public void ConnectEnviroment(UnityEnviroment unityEnviroment, bool autoStart = true)
        {
            _enviroment = unityEnviroment;
            _enviroment.OnDispose += DisconnectEnviroment;

            Application = new GameApplication(unityEnviroment);

            if (autoStart)
                Application.Start();
        }

        private void DisconnectEnviroment()
        {
            _enviroment.OnDispose -= DisconnectEnviroment;
            Application.Dispose();
        }
    }
}

