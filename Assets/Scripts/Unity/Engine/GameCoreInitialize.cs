using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Languages;
using Game.Assets.Scripts.Game.Logic.Infrastructure;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using GameUnity.Assets.Scripts.Unity.Engine.Definitions;
using System;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class GameCoreInitialize : MonoBehaviour
    {
        public static bool IsGameExit { get; private set; }
        private UnityControls _controls = new UnityControls();
        private GameTime _time = new GameTime();
        private UnityEnviroment _enviroment;

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
            UnityControls.Controls = _controls;
            _enviroment = new UnityEnviroment(new LevelsManager(),
                new AssetsLoader(),
                new GameDefinitions(new UnityDefinitions()),
                new GameControls(_controls),
                _time);
            _enviroment.OnDispose += _core_OnDispose;

            IInfrastructure.Default.ConnectEnviroment(_enviroment);
        }

        protected void OnApplicationQuit()
        {
            _enviroment.OnDispose -= _core_OnDispose;
            IsGameExit = true;
            _enviroment.Dispose();
        }

        private void _core_OnDispose()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        protected void OnDestroy()
        {
            if (!_enviroment.IsDisposed)
                throw new Exception("Model is not disposed for some reasons");
        }

        protected void Update()
        {
            _time.MoveTime(UnityEngine.Time.deltaTime);
            _controls.Update();
        }

    }
}