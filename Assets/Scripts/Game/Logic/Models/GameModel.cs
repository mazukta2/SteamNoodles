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
        public event Action<IGameLevel> OnLevelCreated = delegate { };
        public IGameLevel CurrentLevel { get; private set; }

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

            IGameLevel.Default = CurrentLevel;
            if (IGameLevel.Default is IStageLevelService bl) IStageLevelService.Default = bl;

            OnLevelCreated(CurrentLevel);
        } 

        private void DestroyLevel()
        {
            if (CurrentLevel != null)
            {
                CurrentLevel.Dispose();
                CurrentLevel = null;
                IGameLevel.Default = null;
                IStageLevelService.Default = null;
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
