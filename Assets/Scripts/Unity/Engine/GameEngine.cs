using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using GameUnity.Assets.Scripts.Unity.Engine.Definitions;
using System;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class GameEngine : IGameEngine
    {
        public ILevelsManager Levels { get; private set; } 
        public IAssets Assets { get; private set; } = new AssetsLoader();
        public IDefinitions Definitions { get; private set; } = new GameDefinitions();
        public IControls Controls => _controls;

        public GameTime Time { get; } = new GameTime();

        private UnityControls _controls = new UnityControls();

        public GameEngine()
        {
            Levels = new LevelsManager(this);
        }

        public void Update()
        {
            _controls.Update();
            Time.MoveTime(UnityEngine.Time.deltaTime);
        }
    }
}
