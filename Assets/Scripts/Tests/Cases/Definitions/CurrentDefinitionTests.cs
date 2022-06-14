using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services;
using Game.Assets.Scripts.Game.Logic.Models.Services.Definitions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using Game.Assets.Scripts.Game.Environment.Engine;

namespace Game.Assets.Scripts.Tests.Cases.Definitions
{
    public class CurrentDefinitionTests
    {
        [Test]
        public void IsDefinitionsLoaded()
        {
            CreateDefinitions();
            DestoryDefinitions();
        }

        [Test]
        public void LevelDefinitions()
        {
            CreateDefinitions();
            var defs = GetService().GetList<LevelDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var level in defs)
                level.Validate();
            DestoryDefinitions();
        }

        [Test]
        public void CustomerDefinitions()
        {
            CreateDefinitions();
            var defs = GetService().GetList<CustomerDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var custumer in defs)
                custumer.Validate();
            DestoryDefinitions();
        }

        [Test]
        public void ConstructionDefinitions()
        {
            CreateDefinitions();
            var defs = GetService().GetList<ConstructionDefinition>();
            Assert.IsTrue(defs.Count > 0);

            foreach (var level in defs)
                level.Validate();
            DestoryDefinitions();
        }

        [Test]
        public void UnitsSettingsDefinitions()
        {
            CreateDefinitions();
            var def = GetService().Get<UnitsSettingsDefinition>();
            def.Validate();
            DestoryDefinitions();
        }


        [Test]
        public void ConstructionsSettingsDefinitions()
        {
            CreateDefinitions();
            var def = GetService().Get<ConstructionsSettingsDefinition>();
            def.Validate();
            DestoryDefinitions();
        }

        #region Helpers
        private void CreateDefinitions()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var project = currentDirectory.Parent.Parent.Parent;

            var services = new ServiceManager();
            IModelServices.Default = services;

            var definitionService = new DefinitionsService(services, 
                new GameDefinitions(new FileDefinitions(new DirectoryInfo(project.FullName + "/Assets/Resources/Definitions"))), false);
            services.Add(definitionService);

        }

        private void DestoryDefinitions()
        {
            ((IDisposable)IModelServices.Default).Dispose();
        }

        private DefinitionsService GetService()
        {
            return IModelServices.Default.Get<DefinitionsService>();
        }

        public class FileDefinitions : IDefinitions
        {
            private DirectoryInfo _definitionFolder;

            public FileDefinitions(DirectoryInfo definitionFolder)
            {
                _definitionFolder = definitionFolder;
            }

            public string LoadResourceTextfile(string path)
            {
                return File.ReadAllText(_definitionFolder.FullName +"/"+ path + ".json");
            }

            public string[] GetDefintionPaths(string folder)
            {
                var list = _definitionFolder.GetDirectories().First(x => x.Name == folder).GetFiles();
                return list.Where(x => x.Extension == ".json").Select(x => Path.GetFileNameWithoutExtension(x.Name)).ToArray();
            }
        }

    }

    #endregion
}
