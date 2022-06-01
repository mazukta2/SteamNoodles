using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BasePresenter<IMainScreenView>
    {
        private IMainScreenView _view;
        //private readonly IPresenterRepository<Construction> _constructions;
        //private StageFlowService _turnManager;
        private static string _lastAnimation;
        private KeyCommand _exitKey;

        public MainScreenPresenter(IMainScreenView view) 
            : this(view, IGameKeysManager.Default)
        {

        }

        public MainScreenPresenter(IMainScreenView view, IGameKeysManager gameKeysManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            //_constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            //_turnManager = turnManager ?? throw new ArgumentNullException(nameof(turnManager));

            //_constructions.OnAdded += HandleOnAdded;
            _view.NextWaveButton.SetAction(NextWaveClick);
            _view.FailWaveButton.SetAction(FailWaveClick);
            //_turnManager.OnDayFinished += HandleOnDayFinished;
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;
            UpdateWaveProgress();
        }

        protected override void DisposeInner()
        {
            //_constructions.OnAdded -= HandleOnAdded;
            //_turnManager.OnDayFinished -= HandleOnDayFinished;
            _exitKey.OnTap -= OnExitTap;
        }

        private void NextWaveClick()
        {
            //_turnManager.WinWave();
            //UpdateWaveProgress();
        }

        private void FailWaveClick()
        {
            //_turnManager.FailWave();
            //UpdateWaveProgress();
        }

        private void HandleOnDayFinished()
        {
            ScreenManagerPresenter.Default.GetCollection<CommonScreens>().Open<IDayEndedScreenView>();
        }

        private void HandleOnAdded(EntityLink<Construction> arg1, Construction arg2)
        {
            UpdateWaveProgress();
        }

        private void UpdateWaveProgress()
        {
            //_view.NextWaveButton.IsActive = _turnManager.CanNextWave();
            //_view.FailWaveButton.IsActive = _turnManager.CanFailWave();
            //_view.NextWaveProgress.Value = _turnManager.GetWaveProgress();

            //var animation = GetCurrentWaveButtonAnimation().ToString();
            //if (string.IsNullOrEmpty(_lastAnimation) || animation != _lastAnimation)
            //{
            //    _lastAnimation = GetCurrentWaveButtonAnimation().ToString();
            //    _view.WaveButtonAnimator.Play(_lastAnimation);
            //}
            //else
            //{
            //    _view.WaveButtonAnimator.SwitchTo(_lastAnimation);
            //}
        }

        private WaveButtonAnimations GetCurrentWaveButtonAnimation()
        {
            //if (_turnManager.CanFailWave())
            //    return WaveButtonAnimations.FailWave;
            //else if (_turnManager.CanProcessNextWave())
            //    return WaveButtonAnimations.NextWave;
            //else
            //    return WaveButtonAnimations.None;
            return WaveButtonAnimations.None;
        }

        private void OnExitTap()
        {
            ScreenManagerPresenter.Default.Open<IGameMenuScreenView>(x => new GameMenuScreenPresenter(x));
        }

        public enum WaveButtonAnimations
        {
            None,
            NextWave,
            FailWave
        }
    }
}
