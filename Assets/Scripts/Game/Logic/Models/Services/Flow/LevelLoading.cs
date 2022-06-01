using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Flow
{
    public class LevelLoading : Disposable
    {
        public LevelsState State { get; private set; }
        public IViewsCollection Views { get; private set; }

        public event Action OnLoaded = delegate { };

        private LevelDefinition _prototype;
        private ILevelsManager _levelManager;
        private readonly IGame _model;

        public LevelLoading(ILevelsManager levelsManager, IGame model)
        {
            _levelManager = levelsManager ?? throw new ArgumentNullException(nameof(levelsManager));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _model.OnLevelCreated += HandleOnLevelCreated;
            _model.OnLevelDestroyed += HandleOnLevelDestroyed;
            _levelManager.OnLoadFinished += HandleOnFinished;
        }

        protected override void DisposeInner()
        {
            _model.OnLevelCreated -= HandleOnLevelCreated;
            _model.OnLevelDestroyed -= HandleOnLevelDestroyed;
            _levelManager.OnLoadFinished -= HandleOnFinished;
        }

        private void HandleOnLevelCreated(IGameLevel level)
        {
            Load(level.Definition);
        }

        private void HandleOnLevelDestroyed()
        {
            Unload();
        }

        private void Load(LevelDefinition levelDefinition)
        {
            if (State == LevelsState.IsLoaded)
                Unload();
            if (State == LevelsState.IsLoading)
                throw new Exception("Can't load level during loading");

            State = LevelsState.IsLoading;

            Views = new ViewsCollection();
            _prototype = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
            _levelManager.Load(levelDefinition, Views);
        }

        private void Unload()
        {
            if (State != LevelsState.IsLoaded)
                throw new Exception("Wrong state");

            State = LevelsState.None;
            Views.Dispose();
            _levelManager.Unload();
        }

        private void HandleOnFinished()
        {
            new ViewsInitializer(Views).Init();
            _prototype.Starter.Start();

            State = LevelsState.IsLoaded;
            OnLoaded();
        }

        public enum LevelsState
        {
            None,
            IsLoading,
            IsLoaded
        }
    }
}
