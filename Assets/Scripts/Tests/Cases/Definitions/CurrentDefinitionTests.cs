using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Tests.Environment;
using Game.Tests.Controllers;
using NUnit.Framework;
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
            var defs = IDefinitions.Default.GetList<LevelDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var level in defs)
                level.Validate();

            core.Dispose();
        }

        [Test]
        public void CustomerDefinitions()
        {
            var core = CreateCore();
            var defs = IDefinitions.Default.GetList<CustomerDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var level in defs)
                level.Validate();

            core.Dispose();
        }

        [Test]
        public void ConstructionDefinitions()
        {
            var core = CreateCore();
            var defs = IDefinitions.Default.GetList<ConstructionDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var level in defs)
                level.Validate();

            core.Dispose();
        }

        [Test]
        public void UnitsSettingsDefinitions()
        {
            var core = CreateCore();
            var def = IDefinitions.Default.Get<UnitsSettingsDefinition>();
            def.Validate();
            core.Dispose();
        }

        #region Helpers
        private Core CreateCore()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var project = currentDirectory.Parent.Parent.Parent;
            IDefinitions.Default = new FileDefinitions(new DirectoryInfo(project.FullName + "/Assets/Resources/Definitions"));
            var engine = new GameEngineDefinitionsTest();
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
        public ControlsInTests Controls { get; }
        public GameTime Time { get; private set; } = new GameTime();

        ILevelsManager IGameEngine.Levels => Levels;
        IControls IGameEngine.Controls => Controls;
        public LocalizationManager Localization { get; }

        public GameEngineDefinitionsTest()
        {
            Levels = new LevelsManagerInTests(this);
            Controls = new ControlsInTests();
        }

        public void Dispose()
        {
            Levels.Dispose();
        }
    }
    #endregion
}
