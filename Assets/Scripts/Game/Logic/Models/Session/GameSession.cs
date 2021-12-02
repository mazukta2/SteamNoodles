using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Tests.Mocks.Prototypes.Levels;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Session
{
    public class GameSession : Disposable
    {
        public event Action OnLoaded = delegate { };

        public LevelLoading Loading { get; private set; }
        public GameLevel CurrentLevel { get; private set; }
        public SessionRandom Random { get; private set; } = new SessionRandom();
        public GameTime Time { get; private set; } = new GameTime();

        public GameSession(ILevelsController controller)
        {
            _levels = controller;
        }

        protected override void DisposeInner()
        {
            if (Loading != null) Loading.Dispose();
            if (CurrentLevel != null) CurrentLevel.Dispose();
            Time.Dispose();
        }

        public LevelLoading LoadLevel(ILevelSettings levelProto)
        {
            if (CurrentLevel != null) throw new Exception("Need to unload previous level before loading new one");
            if (Loading != null) throw new Exception("Cant load while loading");

            Loading = new LevelLoading(_levels, levelProto, LevelLoaded);
            return Loading;
        }

        private ILevelsController _levels;

        private void LevelLoaded()
        {
            CurrentLevel = new GameLevel(Loading.Prototype, Random, Time);
            Loading.Dispose();
            Loading = null;

            OnLoaded();
        }

    }
}