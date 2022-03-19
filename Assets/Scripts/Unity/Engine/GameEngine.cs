using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
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

        private UnityControls _controls = new UnityControls();

        public GameEngine()
        {
            Levels = new LevelsManager(this);
        }

        public void Update()
        {
            _controls.Update();
        }
    }
}
