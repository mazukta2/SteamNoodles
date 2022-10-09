using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public class GameModel : Disposable, IGame
    {
        public event Action OnLevelDestroyed = delegate { };
        public event Action<ILevel> OnLevelCreated = delegate { };
        public ILevel CurrentLevel { get; private set; }

        public GameModel()
        {
            IGameSession.Default = new GameSession(this);
        }

        protected override void DisposeInner()
        {
            DestroyLevel();

            IGameSession.Default.Dispose();
            IGameSession.Default = null;
        }

        public void StartNewGame()
        {
            StartLevel(IGameDefinitions.Default.Get<MainDefinition>().StartLevel);
        }

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

        public void Exit()
        {
            Dispose();
        }
    }

}
