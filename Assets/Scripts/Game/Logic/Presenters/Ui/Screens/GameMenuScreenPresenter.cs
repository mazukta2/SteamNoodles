﻿using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class GameMenuScreenPresenter : BasePresenter<IGameMenuScreenView>
    {
        private IGameMenuScreenView _view;
        private GameService _game;
        private KeyCommand _exitKey;
        private readonly IGameKeysManager _keysManager;

        public GameMenuScreenPresenter(IGameMenuScreenView view) : 
            this(view, 
                IPresenterServices.Default.Get<GameService>(), 
                IGameKeysManager.Default)
        {
        }

        public GameMenuScreenPresenter(IGameMenuScreenView view, GameService game, IGameKeysManager keysManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
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
            ScreenManagerPresenter.Default.Open<IMainScreenView>(x => new MainScreenPresenter(x));
        }

        private void ExitGameClick()
        {
            _game.Exit();
        }

        private void OnExitTap()
        {
            CloseClick();
        }

    }
}
