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

        public GameTime Time { get; } = new GameTime();


        public GameEngine()
        {
            Levels = new LevelsManager(this);
        }

        public void Update()
        {
            Time.MoveTime(UnityEngine.Time.deltaTime);
        }

        public void Dispose()
        {
        }
    }
}
