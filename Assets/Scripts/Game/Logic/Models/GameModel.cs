using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using System;

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
            CurrentLevel = level.Starter.CreateModel(level);

            ILevel.Default = CurrentLevel;
            if (ILevel.Default is IStageLevel bl) IStageLevel.Default = bl;

            OnLevelCreated(CurrentLevel);
        } 

        private void DestroyLevel()
        {
            if (CurrentLevel != null)
            {
                CurrentLevel.Dispose();
                CurrentLevel = null;
                ILevel.Default = null;
                IStageLevel.Default = null;
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
