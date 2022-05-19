using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class LevelLoading : Disposable
    {
        public LevelsState State { get; set; }
        public event Action<ILevel, IViewsCollection> OnLoaded = delegate { };

        private LevelDefinition _prototype;
        private ILevelsManager _levelManager;

        public LevelLoading(ILevelsManager levelsManager)
        {
            _levelManager = levelsManager ?? throw new ArgumentNullException(nameof(levelsManager));
        }

        public void Load(LevelDefinition levelDefinition)
        {
            if (State == LevelsState.IsLoaded)
                Unload();
            if (State == LevelsState.IsLoading)
                throw new Exception("Can't load level during loading");

            State = LevelsState.IsLoading;

            _prototype = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
            _levelManager.Load(levelDefinition, HandleOnFinished);
        }

        public void Unload()
        {
            if (State != LevelsState.IsLoaded)
                throw new Exception("Wrong state");

            State = LevelsState.None;
            _levelManager.Unload();
            ILevel.Default.Dispose();
            ILevel.Default = null;
            IBattleLevel.Default = null;
        }

        private void HandleOnFinished(IViewsCollection lvl)
        {
            ILevel.Default = _prototype.Starter.CreateModel(_prototype);
            if (ILevel.Default is IBattleLevel bl) IBattleLevel.Default = bl;

            new ViewsInitializer(lvl).Init();
            _prototype.Starter.Start();

            State = LevelsState.IsLoaded;
            OnLoaded(ILevel.Default, lvl);
        }

        public enum LevelsState
        {
            None,
            IsLoading,
            IsLoaded
        }
    }
}
