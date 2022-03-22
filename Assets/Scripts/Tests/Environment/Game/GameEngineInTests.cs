using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Models.Time;
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
        public GameTime Time { get; private set; } = new GameTime();

        IDefinitions IGameEngine.Definitions => Settings;
        IAssets IGameEngine.Assets => Assets;
        ILevelsManager IGameEngine.Levels => Levels;
        IControls IGameEngine.Controls => Controls;


        public GameEngineInTests()
        {
            Levels = new LevelsManagerInTests(this);
            Settings = new DefinitionsInTests();
            Assets = new AssetsInTests();
            Controls = new ControlsInTests();
        }
    }
}
