namespace Game.Assets.Scripts.Tests.Managers.Game
{
    /*
    public class TestGameBuild : IDisposable
    {
        public GameModel Game => Core?.Game;
        public TestGameEngine Engine { get; private set; }
        public Core Core { get; private set; }

        private List<IDisposable> _toDispose = new List<IDisposable>();

        public TestGameBuild(Core core, TestGameEngine gameEngine)
        {
            Core = core;
            Engine = gameEngine;
        }

        public void LoadLevel(TestLevelDefinition loadLevel)
        {
            var session = Game.CreateSession();
            var levelLoading = session.LoadLevel(loadLevel);
            GameLevel newLevel = null;

            levelLoading.OnLoaded += HandleOnLoaded;
            Engine.Levels.FinishLoading();
            levelLoading.OnLoaded -= HandleOnLoaded;

            _toDispose.Add(newLevel);
            _toDispose.Add(session);

            void HandleOnLoaded(GameLevel level)
            {
                newLevel = level;
            }
        }

        public void Dispose()
        {
            foreach (var item in _toDispose)
                item.Dispose();
            _toDispose.Clear();

            Core.Dispose();
            Engine.Assets.Dispose();
            Engine.Levels.Dispose();

            Core = null;
            Engine = null;
        }
    }
    */
}
