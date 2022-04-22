using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Tests.Environment;
using System;

namespace Game.Tests.Controllers
{
    public class GameEngineInTests : IGameEngine
    {
        public LevelsManagerInTests Levels { get; }
        public ControlsInTests Controls { get; }
        public GameTime Time { get; private set; } = new GameTime();

        ILevelsManager IGameEngine.Levels => Levels;
        IControls IGameEngine.Controls => Controls;

        public GameEngineInTests()
        {
            Levels = new LevelsManagerInTests(this);
            Controls = new ControlsInTests();
        }

        public void Dispose()
        {
            Levels.Dispose();
        }
    }
}
