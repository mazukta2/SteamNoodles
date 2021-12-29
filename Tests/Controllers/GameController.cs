using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Tests.Mocks.Settings.Levels;
using Game.Tests.Mocks.Views.Game;
using System;

namespace Game.Tests.Controllers
{
    public class GameController : IGameController
    {
        private Action<float> _moveTime;

        public GameModel Model { get; private set; }
        public GameView View { get; private set; }
        public GamePresenter Presenter { get; private set; }

        public LevelLoadingController Levels { get; } 
        ILevelsController IGameController.Levels => Levels;

        public GameController()
        {
            Levels = new LevelLoadingController(this);
            StartGame();
        }

        public void StartGame()
        {
            Model = new GameModel(this);
            View = new GameView();
            Presenter = new GamePresenter(Model, View);
            CreateSession();
        }

        public void Exit()
        {
            Model.Dispose();
        }

        private void CreateSession()
        {
            Model.CreateSession();
        }

        public void PushTime(float v)
        {
            _moveTime(v);
        }

        public (GameLevel, LevelPresenter, ILevelView) LoadLevel()
        {
            return LoadLevel(new LevelSettings());
        }

        public (GameLevel, LevelPresenter, ILevelView) LoadLevel(LevelSettings proto)
        {
            Model.Session.LoadLevel(proto);
            Levels.Finish();

            Model.Session.Random.SetSeed(23234);

            return (Model.Session.CurrentLevel, Presenter.Session.CurrentLevel, View.Session.Value.GetCurrentLevel());
        }

        public void SetTimeMover(Action<float> moveTime)
        {
            _moveTime = moveTime;
        }
    }
}
