﻿using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Levels;
using Game.Tests.Mocks.Prototypes.Levels;
using Game.Tests.Mocks.Views.Game;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Presenters;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Tests.Controllers
{
    public class GameController : IGameController
    {
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

        public (GameLevel, LevelPresenter, ILevelView) LoadLevel()
        {
            return LoadLevel(new TestLevelPrototype());
        }

        public (GameLevel, LevelPresenter, ILevelView) LoadLevel(TestLevelPrototype proto)
        {
            Model.Session.LoadLevel(proto);
            Levels.Finish();

            return (Model.Session.CurrentLevel, Presenter.Session.CurrentLevel, View.Session.Value.CurrentLevel.Value);
        }
    }
}
