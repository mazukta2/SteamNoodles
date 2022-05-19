using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public class GameModel : Disposable
    {
        public event Action OnLevelDestroyed = delegate { };
        public event Action<ILevel> OnLevelCreated = delegate { };
        public ILevel CurrentLevel { get; private set; }

        public GameModel()
        {
            IGameSession.Default = new GameSession();
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
            if (ILevel.Default is IBattleLevel bl) IBattleLevel.Default = bl;

            OnLevelCreated(CurrentLevel);
        }

        private void DestroyLevel()
        {
            if (CurrentLevel != null)
            {
                CurrentLevel.Dispose();
                CurrentLevel = null;
                ILevel.Default = null;
                IBattleLevel.Default = null;
                OnLevelDestroyed();
            }
        }

        public void SetLevel(LevelDefinition level)
        {
            StartLevel(level);
        }
    }

}
