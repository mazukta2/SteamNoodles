using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Tests.Environment;
using Game.Tests.Controllers;
using System;

namespace Game.Assets.Scripts.Tests.Environment.Game
{
    public class GameEngineMock : IGameEngine
    {
        public LevelsManagerInTests Levels { get; }
        public GameTime Time { get; private set; } = new GameTime();

        ILevelsManager IGameEngine.Levels => Levels;

        public GameEngineMock()
        {
            Levels = new LevelsManagerInTests(this);
        }

        public void Dispose()
        {
            Levels.Dispose();
        }
    }
}
