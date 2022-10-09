using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class LevelLoading : Disposable
    {
        public LevelsState State { get; private set; }
        public IViewsCollection Views { get; private set; }

        public event Action OnLoaded = delegate { };

        private ILevel _levelModel;
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

        private void HandleOnLevelCreated(ILevel level)
        {
            Load(level);
        }

        private void HandleOnLevelDestroyed()
        {
            Unload();
        }

        private void Load(ILevel level)
        {
            if (State == LevelsState.IsLoaded)
                Unload();
            if (State == LevelsState.IsLoading)
                throw new Exception("Can't load level during loading");

            State = LevelsState.IsLoading;

            Views = new ViewsCollection();
            _levelModel = level ?? throw new ArgumentNullException(nameof(level));
            _levelManager.Load(_levelModel, Views);
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
            _levelModel.Start();

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
