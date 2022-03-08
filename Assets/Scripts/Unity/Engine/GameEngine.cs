using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using GameUnity.Assets.Scripts.Unity.Engine.Definitions;
using System;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class GameEngine : IGameEngine
    {
        public ILevelsManager Levels { get; private set; } = new LevelsManager();
        public IDefinitions Settings { get; private set; } = new GameDefinitions();
        public IAssets Assets { get; private set; } = new AssetsLoader();

        private Action<float> _moveTime;
        public void SetTimeMover(Action<float> moveTime)
        {
            _moveTime = moveTime;
        }

        public void Update()
        {
            _moveTime?.Invoke(Time.deltaTime);
        }
    }
}
