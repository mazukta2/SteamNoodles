using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
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
        }

        protected void OnApplicationQuit()
        {
            IsGameExit = true;
            _core.Dispose();
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