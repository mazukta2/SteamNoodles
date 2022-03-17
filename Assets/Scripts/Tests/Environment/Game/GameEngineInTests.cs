using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Tests.Environment;
using System;

namespace Game.Tests.Controllers
{
    public class GameEngineInTests : IGameEngine
    {
        public LevelsManagerInTests Levels { get; }
        public DefinitionsInTests Settings { get; }
        public AssetsInTests Assets { get; }
        public ControlsInTests Controls { get; }

        IDefinitions IGameEngine.Definitions => Settings;
        IAssets IGameEngine.Assets => Assets;
        ILevelsManager IGameEngine.Levels => Levels;
        IControls IGameEngine.Controls => Controls;

        private Action<float> _moveTime;

        public GameEngineInTests()
        {
            Levels = new LevelsManagerInTests(this);
            Settings = new DefinitionsInTests();
            Assets = new AssetsInTests();
            Controls = new ControlsInTests();
        }

        public void SetTimeMover(Action<float> moveTime)
        {
            _moveTime = moveTime;
        }
    }
}
