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
            CreateDefinitions();
        }

        [Test]
        public void LevelDefinitions()
        {
            CreateDefinitions();
            var defs = IDefinitions.Default.GetList<LevelDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var level in defs)
                level.Validate();
        }

        [Test]
        public void CustomerDefinitions()
        {
            CreateDefinitions();
            var defs = IDefinitions.Default.GetList<CustomerDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var custumer in defs)
                custumer.Validate();
        }

        [Test]
        public void ConstructionDefinitions()
        {
            CreateDefinitions();
            var defs = IDefinitions.Default.GetList<ConstructionDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var level in defs)
                level.Validate();
        }

        [Test]
        public void UnitsSettingsDefinitions()
        {
            CreateDefinitions();
            var def = IDefinitions.Default.Get<UnitsSettingsDefinition>();
            def.Validate();
        }


        [Test]
        public void ConstructionsSettingsDefinitions()
        {
            CreateDefinitions();
            var def = IDefinitions.Default.Get<ConstructionsSettingsDefinition>();
            def.Validate();
        }

        #region Helpers
        private void CreateDefinitions()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var project = currentDirectory.Parent.Parent.Parent;
            IDefinitions.Default = new FileDefinitions(new DirectoryInfo(project.FullName + "/Assets/Resources/Definitions"));
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

    #endregion
}
