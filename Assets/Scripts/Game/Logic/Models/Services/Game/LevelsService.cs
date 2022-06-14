using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Game
{
    public class LevelsService : Disposable, IService
    {
        private IRepository<Level> _levels;
        private Level _firstLevel;
        private Level _currentLevel;
        private ViewsCollection _views;
        private LevelsState _state;
        private ILevelsManager _levelsManager;

        public LevelsService(ILevelsManager levelsManager,
            IRepository<Level> levels,
            Level firstLevel)
        {
            _levelsManager = levelsManager;
            _levels = levels ?? throw new ArgumentNullException(nameof(levels));

            foreach (var level in levels.Get())
            {
                if (level.Id == firstLevel.Id)
                    _firstLevel = level;
            }

            if (_firstLevel == null)
                _firstLevel = _levels.Add(firstLevel);
        }


        public LevelsService(ServiceManager services, ILevelsManager levelsManager, 
            IRepository<Level> levels,
            IReadOnlyCollection<LevelDefinition> levelDefinitions,
            LevelDefinition firstLevel)
        {
            _levelsManager = levelsManager;
            _levels = levels ?? throw new ArgumentNullException(nameof(levels));

            foreach (var levelDefinition in levelDefinitions)
            {
                var link = _levels.Add(CreateEntity(levelDefinition, services));
                if (levelDefinition == firstLevel)
                    _firstLevel = link;
            }

            if (_firstLevel == null)
                _firstLevel = _levels.Add(CreateEntity(firstLevel, services));
        }

        protected override void DisposeInner()
        {
            _levelsManager.OnLoadFinished -= HandleOnFinished;
        }

        private Level CreateEntity(LevelDefinition definition, ServiceManager services)
        {
            if (definition.Starter == null)
                return new Level(definition);

            return definition.Starter.CreateEntity(definition, services);
        }

        public void StartFirstLevel()
        {
            Load(_firstLevel);
        }


        public void ReloadCurrentLevel()
        {
            var level = GetCurrentLevel();
            Unload();
            Load(level);
        }

        public Level GetCurrentLevel()
        {
            return _currentLevel;
        }

        public LevelsState GetCurrentState()
        {
            return _state;
        }

        private void Load(Level level)
        {
            if (_state == LevelsState.IsLoaded)
                Unload();

            if (_state == LevelsState.IsLoading)
                throw new Exception("Can't load level during loading");

            _state = LevelsState.IsLoading;

            _currentLevel = level;

            _views = new ViewsCollection();
            _levelsManager.OnLoadFinished += HandleOnFinished;
            _levelsManager.Load(level.SceneName, _views);
        }

        private void Unload()
        {
            if (_currentLevel == null)
                return;

            if (_state != LevelsState.IsLoaded)
                throw new Exception("Wrong state");

            _currentLevel = null;
            _state = LevelsState.None;
            _views.Dispose();
            _levelsManager.Unload();
        }

        private void HandleOnFinished()
        {
            _levelsManager.OnLoadFinished -= HandleOnFinished;

            _currentLevel.StartServices();
            new ViewsInitializer(_views).Init();
            _currentLevel.StartLevel();

            _state = LevelsState.IsLoaded;
        }

        public enum LevelsState
        {
            None,
            IsLoading,
            IsLoaded
        }
    }

}
