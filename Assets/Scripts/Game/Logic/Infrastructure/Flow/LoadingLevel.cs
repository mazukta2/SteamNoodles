﻿using System;
using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Infrastructure.Mapping;
using Game.Assets.Scripts.Game.Logic.Libs.Stateless.Graph;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using static Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions.HandPresenter;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure.Flow
{
    public class LoadingLevel : Disposable
    {
        public event Action<CurrentLevel> OnLoaded = delegate { };

        private ILevelsManager _levelManager;
        private LevelDefinition _definiton;
        private DefaultViews _views;
        private DefaultModels _models;
        private ILevel _levelModel;
        private CurrentLevel _result;

        public LoadingLevel(ILevelsManager levelsManager, LevelDefinition definition)
        {
            _levelManager = levelsManager ?? throw new ArgumentNullException(nameof(levelsManager));
            _definiton = definition ?? throw new ArgumentNullException(nameof(definition));

            _levelManager.OnLoadFinished += HandleOnFinished;

            _views = new DefaultViews();
            _models = new DefaultModels();

            _levelModel = definition.Variation.CreateModel(definition, _models);

            _levelManager.Load(_levelModel.SceneName, _views);
        }


        protected override void DisposeInner()
        {
            if (_result == null)
            {
                _levelModel.Dispose();
                _levelModel = null;
            }
            _levelManager.OnLoadFinished -= HandleOnFinished;
        }

        public bool IsLoaded => _result != null;

        public CurrentLevel GetResult()
        {
            if (_result == null)
                throw new Exception("No scene is loaded");

            return _result;
        }

        private void HandleOnFinished()
        {
            _result = new CurrentLevel(_levelManager, _levelModel, _models, _views);

            new ViewsInitializer(_views).Init();
            new ViewPresenterMapping(_views).Init();

            _result.Start();

            OnLoaded(_result);
            Dispose();
        }

        /*
        private void StartLevel(LevelDefinition level)
        {
            DestroyLevel();
            CurrentLevel = level.Variation.CreateModel(level);

            ILevel.Default = CurrentLevel;
            if (ILevel.Default is IMainLevel bl) IMainLevel.Default = bl;

            OnLevelCreated(CurrentLevel);
        }

        private void DestroyLevel()
        {
            if (CurrentLevel != null)
            {
                CurrentLevel.Dispose();
                CurrentLevel = null;
                ILevel.Default = null;
                IMainLevel.Default = null;
                OnLevelDestroyed();
            }
        }

        public void SetLevel(LevelDefinition level)
        {
            StartLevel(level);
        }
*/
    }
}
