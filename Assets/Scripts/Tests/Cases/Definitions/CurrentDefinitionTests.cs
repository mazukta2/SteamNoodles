﻿using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Environment.Definitions.List;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Definitions
{
    public class CurrentDefinitionTests
    {
        [Test]
        public void IsDefinitionsLoaded()
        {
            var core = CreateCore();
            core.Dispose();
        }

        [Test]
        public void LevelDefinitions()
        {
            var core = CreateCore();
            var defs = core.Engine.Definitions.GetList<LevelDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var level in defs)
                level.Validate();

            core.Dispose();
        }


        [Test]
        public void CustomerDefinitions()
        {
            var core = CreateCore();
            var defs = core.Engine.Definitions.GetList<CustomerDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var level in defs)
                level.Validate();

            core.Dispose();
        }

        [Test]
        public void ConstructionDefinitions()
        {
            var core = CreateCore();
            var defs = core.Engine.Definitions.GetList<ConstructionDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var level in defs)
                level.Validate();

            core.Dispose();
        }

        #region Helpers
        private Core CreateCore()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var project = currentDirectory.Parent.Parent.Parent;
            var definitions = new DirectoryInfo(project.FullName + "/Assets/Resources/Definitions");

            var engine = new GameEngineDefinitionsTest(new FileDefinitions(definitions));
            var core = new Core(engine);

            return core;
        }

        public class FileDefinitions : BaseDefinitions
        {
            private DirectoryInfo _definitionFolder;

            public FileDefinitions(DirectoryInfo definitionFolder)
            {
                _definitionFolder = definitionFolder;
            }

            protected override string LoadResourceTextfile(string path)
            {
                return File.ReadAllText(_definitionFolder.FullName + "/"+ path + ".json");
            }

            protected override string[] GetDefintionPaths(string folder)
            {
                var list = _definitionFolder.GetDirectories().First(x => x.Name == folder).GetFiles();
                return list.Where(x => x.Extension == ".json").Select(x => folder + "/" + Path.GetFileNameWithoutExtension(x.Name)).ToArray();
            }
        }

    }

    public class GameEngineDefinitionsTest : IGameEngine
    {
        public LevelsManagerInTests Levels { get; }
        public IDefinitions Settings { get; }
        public AssetsInTests Assets { get; }
        public ControlsInTests Controls { get; }
        public GameTime Time { get; private set; } = new GameTime();

        IDefinitions IGameEngine.Definitions => Settings;
        IAssets IGameEngine.Assets => Assets;
        ILevelsManager IGameEngine.Levels => Levels;
        IControls IGameEngine.Controls => Controls;


        public GameEngineDefinitionsTest(IDefinitions definitions)
        {
            Levels = new LevelsManagerInTests(this);
            Settings = definitions;
            Assets = new AssetsInTests();
            Controls = new ControlsInTests();
        }

        public void Dispose()
        {
            Levels.Dispose();
            Assets.Dispose();
        }
    }
    #endregion
}