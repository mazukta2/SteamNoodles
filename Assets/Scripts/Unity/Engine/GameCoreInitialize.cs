using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Models.Session;
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

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _engine = new GameEngine();
            _core = new Core(_engine);

            _session = _core.Game.CreateSession();
            _session.LoadLevel(_engine.Settings.Get<MainDefinition>().StartLevel);
        }

        protected void OnApplicationQuit()
        {
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
        }

    }
}