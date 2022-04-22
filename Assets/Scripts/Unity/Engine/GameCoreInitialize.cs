using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using GameUnity.Assets.Scripts.Unity.Engine.Definitions;
using System;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class GameCoreInitialize : MonoBehaviour
    {
        private GameEngine _engine;
        private Core _core;
        private GameSession _session;
        private UnityControls _controls = new UnityControls();

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _engine = new GameEngine();
            _core = new Core(_engine);

            IAssets.Default = new AssetsLoader();
            IDefinitions.Default = new GameDefinitions();
            ILocalizationManager.Default = new LocalizationManager(IDefinitions.Default, "English");
            IControls.Default = _controls;

            _session = _core.Game.CreateSession();
            _session.LoadLevel(IDefinitions.Default.Get<MainDefinition>().StartLevel);
        }

        protected void OnApplicationQuit()
        {
            ILocalizationManager.Default = null;

            _session.Dispose();
            _core.Dispose();
        }

        protected void OnDestroy()
        {
            if (!_core.IsDisposed)
                throw new Exception("Model is not disposed for some reasons");
        }

        protected void Update()
        {
            _engine.Update();
            _controls.Update();
        }

    }
}