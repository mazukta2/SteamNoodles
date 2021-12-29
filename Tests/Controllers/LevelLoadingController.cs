using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Tests.Mocks.Settings.Levels;
using Game.Tests.Mocks.Views.Game;
using Game.Tests.Mocks.Views.Levels;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Presenters;

namespace Game.Tests.Controllers
{
    public class LevelLoadingController : ILevelsController
    {
        private Action _finished;
        private GameController _gameController;

        public LevelLoadingController(GameController gameController)
        {
            _gameController = gameController;
        }

        public void Load(ILevelSettings prototype, Action onFinished)
        {
            _finished = onFinished;
        }

        public void Finish()
        {
            _finished();
        }
    }
}
