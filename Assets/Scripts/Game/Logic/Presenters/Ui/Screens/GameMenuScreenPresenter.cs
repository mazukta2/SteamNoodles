﻿using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class GameMenuScreenPresenter : BasePresenter<IGameMenuScreenView>, IScreenPresenter
    {
        private IGameMenuScreenView _view;
        private ScreenManagerPresenter _screenManager;
        private Core _game;
        private KeyCommand _exitKey;
        private readonly IGameKeysManager _keysManager;

        public GameMenuScreenPresenter(IGameMenuScreenView view, Core game, IGameKeysManager keysManager, ScreenManagerPresenter screenManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _game = game;
            _keysManager = keysManager ?? throw new ArgumentNullException(nameof(keysManager));
            _view.Close.SetAction(CloseClick);
            _view.ExitGame.SetAction(ExitGameClick);

            _exitKey = keysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;
        }

        protected override void DisposeInner()
        {
            _exitKey.OnTap -= OnExitTap;
        }

        private void CloseClick()
        {
            _screenManager.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

        private void ExitGameClick()
        {
            _game.Dispose();
        }

        private void OnExitTap()
        {
            CloseClick();
        }
    }
}
