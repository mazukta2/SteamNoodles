using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
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
        private Core _core;
        private UnityControls _controls = new UnityControls();
        private GameTime _time = new GameTime();

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);

            var definitions = new GameDefinitions(new UnityDefinitions());
            var localization = new LocalizationManager(definitions, "English");
            _core = new Core(new LevelsManager(), new GameAssets(new AssetsLoader()), definitions, new GameControls(_controls), localization, _time);
            _core.OnDispose += _core_OnDispose;
            IInfrastructure.Default = new DefaultInfrastructure(_core);
            _core.Start();
        }

        protected void OnApplicationQuit()
        {
            _core.OnDispose -= _core_OnDispose;
            IsGameExit = true;
            _core.Dispose();
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
            if (!_core.IsDisposed)
                throw new Exception("Model is not disposed for some reasons");
        }

        protected void Update()
        {
            _time.MoveTime(UnityEngine.Time.deltaTime);
            _controls.Update();
        }

    }
}