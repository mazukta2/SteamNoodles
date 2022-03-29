using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
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

        public class FileDefinitions : IDefinitions
        {
            private Dictionary<string, object> _cached = new Dictionary<string, object>();
            private DirectoryInfo _definitionFolder;

            public FileDefinitions(DirectoryInfo definitionFolder)
            {
                _definitionFolder = definitionFolder;
            }

            public T Get<T>(string id)
            {
                var name = typeof(T).Name;
                var path = name + "/" + id;
                if (_cached.ContainsKey(path))
                    return (T)_cached[path];

                var text = LoadResourceTextfile(path);
                try
                {
                    var item = Activator.CreateInstance(typeof(T));
                    _cached.Add(path, item);
                    JsonConvert.PopulateObject(text, item);
                    return (T)item;
                }
                catch (Exception ex)
                {
                    throw new Exception("Cant deserialize : " + path, ex);
                }
            }

            public T Get<T>()
            {
                var path = typeof(T).Name;
                if (_cached.ContainsKey(path))
                    return (T)_cached[path];

                var text = LoadResourceTextfile(path);
                try
                {
                    var item = Activator.CreateInstance(typeof(T));
                    _cached.Add(path, item);
                    JsonConvert.PopulateObject(text, item);
                    return (T)item;
                }
                catch (Exception ex)
                {
                    throw new Exception("Cant deserialize : " + path, ex);
                }
            }

            public IReadOnlyCollection<T> GetList<T>()
            {
                var folderPath = typeof(T).Name;
                var list = _definitionFolder.GetDirectories().First(x => x.Name == folderPath).GetFiles();
                var result = new List<T>();
                foreach (var item in list)
                {
                    if (item.Extension != ".json")
                        continue;

                    var path = _definitionFolder.FullName + "/" + folderPath + "/" + item.Name;
                    if (_cached.ContainsKey(path))
                        result.Add((T)_cached[path]);
                    else
                    {
                        try
                        {
                            var text = File.ReadAllText(path);

                            var obj = Activator.CreateInstance(typeof(T));
                            _cached.Add(path, obj);
                            JsonConvert.PopulateObject(text, obj);

                            result.Add((T)_cached[path]);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Cant deserialize : " + path, ex);
                        }
                    }
                }
                return result.AsReadOnly();
            }

            private string LoadResourceTextfile(string path)
            {
                return File.ReadAllText(_definitionFolder.FullName + "/"+ path + ".json");
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
