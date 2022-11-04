using System;
using System.IO;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Infrastructure;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Levels;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;
using static Game.Assets.Scripts.Tests.Cases.Definitions.CurrentDefinitionTests;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class RealGameTests
    {
        [Test]
        public void GameStarts()
        {
            var game = new GameCoreInitialize();
            game.Awake();
            
            game.OnApplicationQuit();
        }


        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

        private class GameCoreInitialize 
        {
            public static bool IsGameExit { get; private set; }
            private ControlsMock _controls = new ControlsMock();
            private GameTime _time = new GameTime();
            private LevelsManager _levels;
            private UnityEnviroment _enviroment;

            public void Awake()
            {
                _levels = new LevelsManager();
                _enviroment = new UnityEnviroment(_levels,
                    new AssetsMock(),
                    new GameDefinitions(CreateDefinitions()),
                    _controls,
                    _time);
                _enviroment.OnDispose += _core_OnDispose;
                
                IInfrastructure.Default.ConnectEnviroment(_enviroment);
            }

            public void OnApplicationQuit()
            {
                _enviroment.OnDispose -= _core_OnDispose;
                IsGameExit = true;
                _enviroment.Dispose();
            }

            private void _core_OnDispose()
            {
                if (!IsGameExit)
                    OnApplicationQuit();
            }

            protected void OnDestroy()
            {
                if (!_enviroment.IsDisposed)
                    throw new Exception("Model is not disposed for some reasons");
            }

            protected void Update()
            {
                _time.MoveTime(1f);
                //_controls.Update();
            }

            private IDefinitionsLoader CreateDefinitions()
            {
                var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
                var project = currentDirectory.Parent.Parent.Parent;
                var projectDirectory = new DirectoryInfo(project.FullName).Parent.Parent;
                return new FileDefinitions(new DirectoryInfo(projectDirectory.FullName + "/Assets/Resources/Definitions"));
            }
        }

    }
}

