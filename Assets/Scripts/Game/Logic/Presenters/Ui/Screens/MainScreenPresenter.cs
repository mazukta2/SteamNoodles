﻿using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BasePresenter<IMainScreenView>
    {
        private IMainScreenView _view;
        private ScreenManagerPresenter _screenManager;
        private readonly IPresenterRepository<Construction> _constructions;
        private FlowManager _turnManager;
        private readonly HandPresenter _handPresenter;
        private static string _lastAnimation;
        private KeyCommand _exitKey;

        public MainScreenPresenter(IMainScreenView view, ScreenManagerPresenter screenManager,
            IPresenterRepository<Construction> constructions,
            FlowManager turnManager, HandPresenter handPresenter, IGameKeysManager gameKeysManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _turnManager = turnManager ?? throw new ArgumentNullException(nameof(turnManager));
            _handPresenter = handPresenter;
            _handPresenter.Mode = HandPresenter.Modes.Choose;

            _constructions.OnAdded += HandleOnAdded;
            _view.NextWaveButton.SetAction(NextWaveClick);
            _view.FailWaveButton.SetAction(FailWaveClick);
            _turnManager.OnDayFinished += HandleOnDayFinished;
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;
            UpdateWaveProgress();
        }

        protected override void DisposeInner()
        {
            _constructions.OnAdded -= HandleOnAdded;
            _turnManager.OnDayFinished -= HandleOnDayFinished;
            _exitKey.OnTap -= OnExitTap;
            _handPresenter.Mode = HandPresenter.Modes.Disabled;
        }

        private void NextWaveClick()
        {
            _turnManager.WinWave();
            UpdateWaveProgress();
        }

        private void FailWaveClick()
        {
            _turnManager.FailWave();
            UpdateWaveProgress();
        }

        private void HandleOnDayFinished()
        {
            _screenManager.GetCollection<CommonScreens>().Open<IDayEndedScreenView>();
        }

        private void HandleOnAdded(EntityLink<Construction> arg1, Construction arg2)
        {
            UpdateWaveProgress();
        }

        private void UpdateWaveProgress()
        {
            _view.NextWaveButton.IsActive = _turnManager.CanNextWave();
            _view.FailWaveButton.IsActive = _turnManager.CanFailWave();
            _view.NextWaveProgress.Value = _turnManager.GetWaveProgress();

            var animation = GetCurrentWaveButtonAnimation().ToString();
            if (string.IsNullOrEmpty(_lastAnimation) || animation != _lastAnimation)
            {
                _lastAnimation = GetCurrentWaveButtonAnimation().ToString();
                _view.WaveButtonAnimator.Play(_lastAnimation);
            }
            else
            {
                _view.WaveButtonAnimator.SwitchTo(_lastAnimation);
            }
        }

        private WaveButtonAnimations GetCurrentWaveButtonAnimation()
        {
            if (_turnManager.CanFailWave())
                return WaveButtonAnimations.FailWave;
            else if (_turnManager.CanProcessNextWave())
                return WaveButtonAnimations.NextWave;
            else
                return WaveButtonAnimations.None;
        }

        private void OnExitTap()
        {
            _screenManager.GetCollection<CommonScreens>().Open<IGameMenuScreenView>();
        }

        public enum WaveButtonAnimations
        {
            None,
            NextWave,
            FailWave
        }
    }
}
