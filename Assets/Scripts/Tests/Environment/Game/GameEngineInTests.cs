using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using System;

namespace Game.Tests.Controllers
{
    public class GameEngineInTests : IGameEngine
    {
        public LevelsManagerInTests Levels { get; }
        public DefinitionsInTests Settings { get; }
        public AssetsInTests Assets { get; }

        IDefinitions IGameEngine.Settings => Settings;
        IAssets IGameEngine.Assets => Assets;
        ILevelsManager IGameEngine.Levels => Levels;

        private Action<float> _moveTime;

        public GameEngineInTests()
        {
            Levels = new LevelsManagerInTests();
            Settings = new DefinitionsInTests();
            Assets = new AssetsInTests();
        }

        public void SetTimeMover(Action<float> moveTime)
        {
            _moveTime = moveTime;
        }
    }
}
